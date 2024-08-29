using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

/// <summary>
/// The <c>Countdown</c> class generates a visual countdown that runs an event on its conclusion.
/// </summary>
public class Countdown
{
    private string[] _entries;
    private int _currentIndex;
    private float _scale;
    private float _time;
    private GameObject _gameObject;
    private TextMeshProUGUI _textMeshPro;
    private CancellationTokenSource _cancellationTokenSource;

    private Vector3 _originalScale;
    private float _originalAlpha;

    /// <summary>
    /// Runs the instant the countdown concludes. 
    /// </summary>
    public event Action OnCountdownComplete;

    public Countdown(string[] entries, float scale, float time, GameObject gameObject, TextMeshProUGUI textMeshPro, CancellationTokenSource cts = null)
    {
        _entries = entries;
        _currentIndex = 0;
        _scale = scale;
        _time = time;
        _gameObject = gameObject;
        _textMeshPro = textMeshPro;
        if (cts != null)
        {
            _cancellationTokenSource = cts;
        }
    }

    /// <summary>
    /// Generates the a DOTween sequence for the countdown and plays it. 
    /// </summary>
    public async UniTask Run()
    {
        _textMeshPro.text = _entries[0];
        _gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence(_gameObject);
        _originalScale = _gameObject.transform.localScale;
        _originalAlpha = 1f;

        for (var i = 0; i < _entries.Length - 1; i++)
        {
            _ = sequence.Append(_gameObject.transform.DOScale(_scale, _time));
            _ = sequence.Join(_textMeshPro.DOFade(0f, _time));
            _ = sequence.AppendCallback(Decrement);
        }

        if (_cancellationTokenSource != null)
        {
            _ = sequence.WithCancellation(_cancellationTokenSource.Token);
        }

        await sequence.Play();
        OnCountdownComplete?.Invoke();

        sequence = DOTween.Sequence(_gameObject);
        _ = sequence.Append(_gameObject.transform.DOScale(_scale, _time));
        _ = sequence.Join(_textMeshPro.DOFade(0f, _time));
        _ = sequence.JoinCallback(() => _textMeshPro.text = _entries[_entries.Length - 1]);
        _ = sequence.AppendCallback(Hide);

        if (_cancellationTokenSource != null)
        {
            _ = sequence.WithCancellation(_cancellationTokenSource.Token);
        }

        _ = sequence.Play();
    }

    private void Hide()
    {
        _gameObject.SetActive(false);
    }

    private void Decrement()
    {
        _currentIndex++;
        _textMeshPro.text = _entries[_currentIndex];
        _gameObject.transform.localScale = _originalScale;
        _textMeshPro.color = new Color(_textMeshPro.color.r, _textMeshPro.color.g, _textMeshPro.color.b, _originalAlpha);
    }
}
