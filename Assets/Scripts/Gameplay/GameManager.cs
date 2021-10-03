using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum GAME_STATE
{
    START,
    INTRO,
    GAME,
    PAUSE,
    WON
}

public class GameManager : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////

    private static GameManager _instance;

    private GAME_STATE _state = GAME_STATE.INTRO;
    private GAME_STATE _lateState = GAME_STATE.START;

    //--GETTERS-&-SETTERS---------------------------------------------------//

    public static bool Paused
    {
        get { return Time.timeScale == 0f; }
        set { Time.timeScale = value ? 0f : 1f; }
    }

    public static GAME_STATE State
    {
        get { return _instance._state; }
    }

    public static Element[] Elements
    {
        get { return _instance._elements; }
    }

    public static int Score { get; private set; } = 0;
    public static int LevelID { get; private set; } = 0;

    //--HIDDEN-REFERENCES---------------------------------------------------//

    [SerializeField] private Element[] _elements;
    [SerializeField] private Deck _deck;
    [SerializeField] private CellGrid _grid;

    [Header("Debug")]
    [SerializeField] private bool _drawGizmos = false;

    //----------------------------------------------------------------------//

    private IEnumerator _endLevelCoroutine = null;

    //////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        if (_instance != null)
            Destroy(_instance);
        else
            _instance = this;
    }

    private void Update()
    {
        OnStateUpdate();
    }

    private void LateUpdate()
    {
        if (_state != _lateState)
            OnStateChanged();

        _lateState = _state;
    }

    //----------------------------------------------------------------------//

    private void OnStateUpdate()
    {
        switch (_state)
        {
            case GAME_STATE.START:
                break;
            case GAME_STATE.INTRO:
                if (Input.anyKeyDown)
                {
                    _state = GAME_STATE.GAME;
                    SoundManager.PlaySound("beep");
                }
                break;
            case GAME_STATE.GAME:
                OnGameUpdate();
                break;
            case GAME_STATE.PAUSE:
                break;
            case GAME_STATE.WON:
                if (Input.anyKeyDown)
                {
                    _state = GAME_STATE.INTRO;
                    SoundManager.PlaySound("beep");
                }
                break;
        }
    }

    private void OnStateChanged()
    {
        switch (_state)
        {
            case GAME_STATE.START:
                break;
            case GAME_STATE.INTRO:
                Paused = true;
                LevelID = 0;
                UIManager.ShowPopup("Intro");
                SoundManager.MusicVolume = .25f;
                break;
            case GAME_STATE.GAME:
                Paused = false;
                UIManager.HidePopups();
                SoundManager.MusicVolume = .1f;
                LoadLevel(LevelID);
                break;
            case GAME_STATE.PAUSE:
                Paused = true;
                break;
            case GAME_STATE.WON:
                Paused = true;
                StopAllCoroutines();
                DOTween.CompleteAll(true);
                UIManager.ShowPopup("Win");
                SoundManager.PlaySound("end", false, .25f);
                SoundManager.PlaySound("win");
                SoundManager.MusicVolume = 0f;
                break;
        }
    }

    private void OnGameUpdate()
    {
        // Calculate score.
        Score = 0;
        for (int x = 0; x < CellGrid.Grid.GetLength(0); x++)
        {
            for (int y = 0; y < CellGrid.Grid.GetLength(1); y++)
            {
                Score += Matches.CheckMatches(x, y);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
            LoadLevel(LevelID);

        if (Deck.IsEmpty && _endLevelCoroutine == null)
        {
            _endLevelCoroutine = EndLevelCoroutine();
            StartCoroutine(_endLevelCoroutine);
        }
    }

    //----------------------------------------------------------------------//

    public void LoadLevel(int levelID)
    {
        LevelID = levelID;

        _deck.Clear();
        _grid.Clear();

        Level level = Data.Levels[LevelID];

        _grid.Assemble(level.grid);
        _deck.Assemble(Data.ShuffleDeck(level.deck));
    }

    //----------------------------------------------------------------------//

    private bool CheckWinCondition()
    {
        Level level = Data.Levels[LevelID];
        return Score >= level.minScore && Score <= level.maxScore;
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos)
            return;
    }

    //----------------------------------------------------------------------//

    private IEnumerator EndLevelCoroutine()
    {
        yield return new WaitForSecondsRealtime(.5f);

        _state = GAME_STATE.PAUSE;

        if (CheckWinCondition())
        {
            foreach (Cell cell in CellGrid.Grid)
                if (cell) cell.Card.transform.DOMove(Vector3.right * 10f, 1f).SetUpdate(true);

            SoundManager.PlaySound("success", false, .5f);

            yield return new WaitForSecondsRealtime(1f);

            LevelID++;
        }
        else
        {
            if (Score > Data.Levels[LevelID].maxScore)
            {
                foreach (Cell cell in CellGrid.Grid)
                    if (cell) cell.Card.transform.DOMove(Random.insideUnitSphere.normalized * 15f, 2f).SetUpdate(true);

                SoundManager.PlaySound("explosion");

                yield return new WaitForSecondsRealtime(2f);
            }
            else
            {
                foreach (Cell cell in CellGrid.Grid)
                    if (cell) cell.Card.transform.DOMoveY(-5f, 1f).SetUpdate(true);

                SoundManager.PlaySound("fail", false, .5f);

                yield return new WaitForSecondsRealtime(1f);
            }
        }

        DOTween.KillAll();

        _endLevelCoroutine = null;
        _state = LevelID < Data.Levels.Length ? GAME_STATE.GAME : GAME_STATE.WON;
    }

    //////////////////////////////////////////////////////////////////////////
}