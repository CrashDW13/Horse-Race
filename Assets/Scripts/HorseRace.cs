using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// The <c>HorseRace</c> class represents a predetermined race between one or more instances of the <c>HorseRacer</c> class.
/// </summary>
public class HorseRace
{
    private List<HorseRacer> _racers;
    private IRaceTimeGenerator _timeGenerator;
    private IRaceWeightedPlacement _placement;
    private IRaceCheckpointGenerator _checkpointGenerator;
    private Vector3 _startingPosition;
    private float _raceDistance;

    private float[] _times;
    /// <summary>
    /// An array of the times of each racer, from fastest to slowest.
    /// </summary>
    public float[] Times { get { return _times; } }
    private int[] _placements;
    /// <summary>
    /// An array of the IDs of each racer, with the index indicating placement.
    /// </summary>
    public int[] Placements { get { return _placements; } }

    /// <summary>
    /// Constructs an instance of the <c>HorseRace</c> class.
    /// </summary>
    /// <param name="racers">A list of all racers involved in the race.</param>
    /// <param name="timeGenerator">An instance of a class that implements <c>IRaceTimeGenerator</c> to generate the times for each racer in the race.</param>
    /// <param name="placement">An instance of a class that implements <c>IRaceWeightedPlacement</c> to generate the placements for each racer in the race.</param>
    /// <param name="checkpointGenerator">An instance of a class that implements <c>IRaceCheckpointGenerator</c> to generate a list of checkpoints for each racer in the race.</param>
    /// <param name="startingPosition">The starting position of the race in world space.</param>
    /// <param name="distance">How far the race should travel in distance along the x-axis.</param>
    public HorseRace(List<HorseRacer> racers, IRaceTimeGenerator timeGenerator, IRaceWeightedPlacement placement, IRaceCheckpointGenerator checkpointGenerator, Vector3 startingPosition, float distance)
    {
        _racers = racers;
        _timeGenerator = timeGenerator;
        _checkpointGenerator = checkpointGenerator;
        _placement = placement;
        _startingPosition = startingPosition;
        _raceDistance = distance;
    }

    /// <summary>
    /// Generates all of the variables related to the race.
    /// </summary>
    public void Setup()
    {
        _times = _timeGenerator.GenerateRaceTimes();
        _placements = _placement.GetPlacements();
        for(var i = 0; i < _placements.Length; i++)
        {
            int id = _placements[i];
            HorseRacer racer = _racers[id];
            List<HorseRaceCheckpoint> checkpoints = _checkpointGenerator.GenerateCheckpoints(i);
            HorseRacerSequence sequence = new HorseRacerSequence();
            Sequence racerSequence = sequence.GenerateSequence(racer.GameObject, checkpoints, _startingPosition, _raceDistance, _times[i]);
            racer.SetSequence(racerSequence);
        }
    }

    /// <summary>
    /// Instructs the racers to start moving.
    /// </summary>
    public void Start()
    {
        foreach (HorseRacer racer in _racers)
        {
            racer.StartRacing();
        }
    }
}
