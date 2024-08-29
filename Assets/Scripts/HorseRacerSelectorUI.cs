using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The <c>HorseRacerSelectorUI</c> class is responsible for visually representing the <c>HorseRacerSelector</c> class. 
/// </summary>
public class HorseRacerSelectorUI : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private HorseRaceManager _raceManager;
 
    [SerializeField] private Image _horsePreview;
    [SerializeField] private TextMeshProUGUI _horseName;
    [SerializeField] private TextMeshProUGUI _horseOrigin;
    [SerializeField] private TextMeshProUGUI _jockeyName;
    [SerializeField] private TextMeshProUGUI _jockeyOrigin;
    [SerializeField] private TextMeshProUGUI _winPercentage;
    [SerializeField] private TextMeshProUGUI _inTheMoneyPercentage;
    [SerializeField] private TextMeshProUGUI _averageWinningDistance;

    [Header("Tweening")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _distance;
    [SerializeField] private float _time;

    private HorseRacerSelector _selector;
    private void Start()
    {
        _selector = new HorseRacerSelector(_raceManager);
        _horsePreview.material = new Material(_horsePreview.material);

        _selector.OnIncrement += SetupDisplay;
        _selector.OnDecrement += SetupDisplay;

        transform.position = new Vector3(transform.position.x, transform.position.y - _distance, transform.position.z);
        transform.DOMoveY(transform.position.y + _distance, _time);
        _canvasGroup.alpha = 0f;
        _canvasGroup.DOFade(1f, _time);

        SetupDisplay();
    }

    /// <summary>
    /// See <c>HorseRacerSelector</c> definition; increments index by one or wraps around to zero if necessary.
    /// </summary>
    public void Increment()
    {
        _selector.Increment();
    }

    /// <summary>
    /// See <c>HorseRacerSelector</c> definition; decrements index by one or wraps around to the last index in the list if necessary. 
    /// </summary>
    public void Decrement()
    {
        _selector.Decrement();
    }

    /// <summary>
    /// See <c>HorseRacerSelector</c> definition; in addition to calling <c>SelectRacer()</c>, creates a countdown and hides the UI. 
    /// </summary>
    public void Select()
    {
        _selector.SelectRacer();
        transform.DOMoveY(transform.position.y - _distance, _time);
        _canvasGroup.DOFade(0f, _time / 2f).OnComplete(Hide);
        _ = _raceManager.Run();
    }

    private void Hide()
    {
        _canvasGroup.gameObject.SetActive(false);
    }

    private void SetupDisplay()
    {
        HorseRacerData data = _selector.Data[_selector.Index];
        _horsePreview.material.SetColor("_RacerColor", data.Graphics.RacerColor);
        _horsePreview.material.SetColor("_HorseColor", data.Graphics.HorseColor);
        HorseRacerDescription description = data.Description;

        _horseName.text = description.HorseName;
        _horseName.color = new Color(data.Graphics.RacerColor.r, data.Graphics.RacerColor.g, data.Graphics.RacerColor.b, 1);

        _horseOrigin.text = "<b>Raised In:</b> " + description.HorseOrigin;
        _jockeyName.text = "<b>Jockey:</b> " + description.JockeyName;
        _jockeyOrigin.text = "<b>Origin:</b> " + description.JockeyOrigin;
        _winPercentage.text = "<b>Win%:</b> " + description.WinPercentage.ToString() + "%";
        _inTheMoneyPercentage.text = "<b>In-The-Money%:</b> " + description.InTheMoneyPercentage.ToString() + "%";
        _averageWinningDistance.text = "<b>AWD:</b> " + description.AverageWinningDistance.ToString();
    }

}
