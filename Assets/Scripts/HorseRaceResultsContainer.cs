using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class HorseRaceResultsContainer : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _distance;
    [SerializeField] private float _time;

    public void Activate()
    {
        gameObject.SetActive(true);

        transform.position = new Vector3(transform.position.x, transform.position.y - _distance, transform.position.z);
        _canvasGroup.alpha = 0;

        transform.DOMoveY(transform.position.y + _distance, _time);
        _canvasGroup.DOFade(1, _time);
    }
}
