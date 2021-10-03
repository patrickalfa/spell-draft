using System.Collections;
using UnityEngine;

[System.Serializable]
public struct AudioNode
{
    public string name;
    public AudioClip sound;
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static float SfxVolume
    {
        get { return _sfxVolume; }

        set
        {
            _sfxVolume = value;
            foreach (AudioSource src in _instance._soundSources)
                src.volume = _sfxVolume;
        }
    }

    public static float MusicVolume
    {
        get { return _instance.GetComponent<AudioSource>().volume; }
        set { _instance.GetComponent<AudioSource>().volume = value; }
    }

    public AudioNode[] AudioNodes;

    private static float _sfxVolume = 1f;

    private ArrayList _soundSources = new ArrayList();
    private static Hashtable _audios = new Hashtable();


    public void PlaySoundUI(string name)
    {
        PlaySound((AudioClip)_audios[name], false, 1f);
    }

    public static void PlaySound(string name, bool loop = false, float volume = 1f, float pitch = 1f)
    {
        PlaySound((AudioClip)_audios[name], loop, volume, pitch);
    }

    public static void PlaySound(AudioClip sound, bool loop = false, float volume = 1f, float pitch = 1f)
    {
        if (sound == null) return;
        foreach (AudioSource src in _instance._soundSources)
        {
            if (!src.isPlaying)
            {
                src.name = sound.name;
                src.loop = loop;
                src.volume = SfxVolume * volume;
                src.clip = sound;
                src.pitch = pitch;
                src.Play();
                return;
            }
        }

        AudioSource newSrc = CreateNewSource();
        newSrc.loop = loop;
        newSrc.volume = _sfxVolume * volume;
        newSrc.PlayOneShot(sound);
    }

    public static void SetChannelVolume(string channel, float volume)
    {
        _instance.transform.Find(channel).GetComponent<AudioSource>().volume = volume;
    }

    private static AudioSource CreateNewSource()
    {
        GameObject temp = new GameObject();
        temp.transform.parent = _instance.transform;
        temp.transform.localPosition = Vector3.zero;

        AudioSource src = temp.AddComponent<AudioSource>();
        _instance._soundSources.Add(src);

        return src;
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(_instance);
        else
            _instance = this;

        GetAudioSources();
        FillAudioDictionary();

        SfxVolume = 1f;
    }

    private void GetAudioSources()
    {
        foreach (Transform child in transform)
        {
            AudioSource src = child.GetComponent<AudioSource>();
            _soundSources.Add(src);
        }
    }

    private void FillAudioDictionary()
    {
        foreach (AudioNode node in AudioNodes)
            if (!_audios.ContainsKey(node.name))
                _audios.Add(node.name, node.sound);
    }
}