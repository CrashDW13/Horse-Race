using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The <c>HorseRaceManager</c> class is the Monobehaviour responsible for storing and distributing data for the <c>HorseRace</c> class.
/// </summary>
public class HorseRaceManager : MonoBehaviour
{
    [Header("Time Generator")]
    [SerializeField] private Vector2 _raceTimeWindow;
    [SerializeField, Range(0, 1)] private float _minIntervalPercentage;
    [SerializeField, Range(0, 1)] private float _maxIntervalPercentage;

    [Header("Checkpoint Generator")]
    [SerializeField] private int _minExtraCheckpoints;
    [SerializeField] private int _maxExtraCheckpoints;
    [SerializeField, Range(0, 1)] private float _checkpointVariation;
    [SerializeField, Range(0, 1)] private float _strengthVariation;

    [Header("Countdown")]
    [SerializeField] private GameObject _countdownObject;
    [SerializeField] private TextMeshProUGUI _countdownText;

    [Header("GameObject Placement")]
    [SerializeField] private Transform _startLine;
    [SerializeField] private Transform _finishLine;
    [SerializeField] private float _laneVerticalGap;

    [Header("Results")]
    [SerializeField] private HorseRaceResultsContainer _resultsObject;
    [SerializeField] private HorseRaceResults[] _results;

    [SerializeField] private List<HorseRacerData> _data;

    private HorseRace _race;

    private Task _resultsTask;
    private CancellationTokenSource _tokenSource;

    /// <summary>
    /// A list of the cosmetic data related to each of the racers.
    /// </summary>
    public List<HorseRacerData> Data { get => _data; }
    private List<HorseRacer> _racers = new();
    private HorseRacer _playerRacer = null;
    private Camera _camera;

    private void Start()
    {
        InstantiateRacers();
        InitializeRace();
        _camera = Camera.main;
    }

    private void InstantiateRacers()
    {
        for (var i = 0; i < _data.Count; i++)
        {
            GameObject racerGameObject = new GameObject();
            racerGameObject.name = _data[i].Description.HorseName;
            racerGameObject.transform.position = new Vector3(_startLine.position.x, _startLine.position.y - (_laneVerticalGap * i), _startLine.position.z);
            HorseRacer racer = _data[i].CreateRacer(ref racerGameObject);

            //  There are several more eloquent ways to do this,
            //  But I wouldn't want to pass a reference to the racer's index through 3 classes *just* for this...
            racer.GameObject.GetComponent<SpriteRenderer>().sortingOrder = i;
            _racers.Add(racer);
        }
    }

    /// <summary>
    /// Sets the "player" racer. The player racer is the one tracked by the camera. 
    /// </summary>
    /// <param name="index"></param>
    public void SetPlayerRacer(int index)
    {
        if (_racers.Count == 0)
        {
            Debug.LogWarning($"{this}: Tried setting player racers when they have not been instantiated.");
            return;
        }

        _playerRacer = _racers[index];
        _camera.transform.parent = _racers[index].GameObject.transform;
    }

    private void InitializeRace()
    {
        IRaceTimeGenerator timeGenerator = new HorseRaceTimeGenerator(_racers.Count, _raceTimeWindow.x, _raceTimeWindow.y, _minIntervalPercentage, _maxIntervalPercentage);
        IRaceWeightedPlacement weightedPlacement = new HorseRaceWeightedPlacement(_racers);
        IRaceCheckpointGenerator checkpointGenerator = new HorseRaceCheckpointGenerator(_minExtraCheckpoints, _maxExtraCheckpoints, _checkpointVariation, _strengthVariation);
        _race = new HorseRace(_racers, timeGenerator, weightedPlacement, checkpointGenerator, _startLine.position, _finishLine.position.x - _startLine.position.x, _tokenSource);
        _race.Setup();
    }

    /// <summary>
    /// Begins the race; tells the GameObjects to start moving.
    /// </summary>
    public async UniTask Run()
    {
        Countdown countdown = new Countdown(new string[] { "3", "2", "1", "GO!" }, 8, 1, _countdownObject, _countdownText, _tokenSource);
        await countdown.Run();
        await _race.Run();
        ShowResults();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ShowResults()
    {
        _resultsObject.Activate();
        for (int i = 0; i < _race.Placements.Length; i++)
        {
            int racerIndex = _race.Placements[i];
            _results[i].SetResults(_data[racerIndex].Graphics.RacerColor, _data[racerIndex].Description.HorseName, _race.Times[i]);
        }
    }

    private void OnDestroy()
    {
        _tokenSource?.Cancel();
    }
}