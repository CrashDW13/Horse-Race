using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>HorseRacer</c> class represents a racer participating in a race. 
/// </summary>
public class HorseRacer
{
    private int _weight;
    private float _raceTime; 
    private HorseRacerData _data;
    private Sequence _sequence;
    private GameObject _gameObject;

    /// <summary>
    /// The weight of the racer that decides their odds at winning the race. 
    /// </summary>
    public int Weight { get { return _weight; } }

    /// <summary>
    /// The GameObject that represents the racer in the scene. 
    /// </summary>
    public GameObject @GameObject { get => _gameObject; }

    public HorseRacer(HorseRacerData data, GameObject gameObject, int weight)
    {
        _gameObject = gameObject;
        _weight = weight; 
        _data = data;
        data.Graphics.Setup(ref gameObject);
    }

    /// <summary>
    /// Sets the sequence that will be used to determine the racer's movement through the race. 
    /// </summary>
    /// <param name="sequence"></param>
    public void SetSequence(Sequence sequence)
    {
        _sequence = sequence;
        _sequence.OnComplete(StopRacing);
    }

    /// <summary>
    /// Starts the racer's movement. 
    /// </summary>
    public void StartRacing()
    {
        _sequence.Play();
        _data.Graphics.RacerAnimator.Play("Running");
    }

    public void StopRacing()
    {
        GameObject.transform.DOMoveX(GameObject.transform.position.x + Random.Range(3f, 6f), 1);
        _data.Graphics.RacerAnimator.Play("Skid");
    }
}
