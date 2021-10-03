using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////
    ///
    private static UIManager _instance;

    [SerializeField] private Transform _popups;
    [SerializeField] private Transform _hud;

    //----------------------------------------------------------------------//

    [Header("Colors")]
    [SerializeField] private Color colorScoreWin;
    [SerializeField] private Color colorScoreFail;

    //----------------------------------------------------------------------//

    private Slider _sliderCurScore;
    private TextMeshProUGUI _textCurScore;
    private Image _sliderHandle;
    private Image _sliderFill;

    private Slider _sliderMinScore;
    private TextMeshProUGUI _textMinScore;

    private TextMeshProUGUI _textMaxScore;

    private TextMeshProUGUI _textLevelTitle;

    //////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        if (_instance != null)
            Destroy(_instance);
        else
            _instance = this;
    }

    private void Start()
    {
        _sliderCurScore = _hud.Find("Score/Current").GetComponent<Slider>();
        _textCurScore = _hud.Find("Score/Current/Handle Slide Area/Handle/Counter").GetComponent<TextMeshProUGUI>();
        _sliderHandle = _hud.Find("Score/Current/Handle Slide Area/Handle").GetComponent<Image>();
        _sliderFill = _hud.Find("Score/Current/Fill Area/Fill").GetComponent<Image>();

        _sliderMinScore = _hud.Find("Score/Min").GetComponent<Slider>();
        _textMinScore = _hud.Find("Score/Min/Handle Slide Area/Handle/Counter").GetComponent<TextMeshProUGUI>();

        _textMaxScore = _hud.Find("Score/Max/Counter").GetComponent<TextMeshProUGUI>();

        _textLevelTitle = _hud.Find("Level").GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        if (GameManager.State != GAME_STATE.GAME) return;

        UpdateScoreCounter();

        _textLevelTitle.text = Data.Levels[GameManager.LevelID].title;
    }

    //----------------------------------------------------------------------//

    public static void ShowPopup(string popupName)
    {
        HidePopups();

        Transform popup = _instance._popups.Find(popupName);

        if (popup == null)
        {
            Debug.LogWarning("Popup \"" + popupName + "\" not found.");
            return;
        }

        popup.gameObject.SetActive(true);
    }

    public static void HidePopups()
    {
        foreach (Transform popup in _instance._popups)
            popup.gameObject.SetActive(false);
    }

    //----------------------------------------------------------------------//

    private void UpdateScoreCounter()
    {
        Level level = Data.Levels[GameManager.LevelID];

        _sliderCurScore.maxValue = level.maxScore;
        _sliderCurScore.value = GameManager.Score;
        _textCurScore.text = GameManager.Score.ToString();

        Color color = GameManager.Score < level.minScore ? Color.white :
            (GameManager.Score > level.maxScore ? colorScoreFail : colorScoreWin);
        _textCurScore.color = _sliderHandle.color = _sliderFill.color = color;


        _sliderMinScore.maxValue = level.maxScore;
        _sliderMinScore.value = level.minScore;
        _textMinScore.text = level.minScore.ToString();

        _textMaxScore.text = level.maxScore.ToString();
        _textMaxScore.enabled = GameManager.Score < level.maxScore;
    }

    //////////////////////////////////////////////////////////////////////////
}
