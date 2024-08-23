using DG.Tweening;
using UnityEngine;

public class HorseRaceResultsContainer : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _distance;
    [SerializeField] private float _time;
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - _distance, transform.position.z);
        _canvasGroup.alpha = 0;

        transform.DOMoveY(transform.position.y + _distance, _time);
        _canvasGroup.DOFade(1, _time);
    }
}
