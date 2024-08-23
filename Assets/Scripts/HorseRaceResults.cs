using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorseRaceResults : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _horseName;
    [SerializeField] private TextMeshProUGUI _time;

    public void SetResults(Color color, string name, float time)
    {
        _background.color = new Color(color.r, color.g, color.b, _background.color.a);
        _horseName.text = name;
        _time.text = time.ToString() + "s";
    }
}
