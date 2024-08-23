using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>HorseRaceCheckpointGenerator</c> class provides a default implementation on building checkpoints for a racer during a race. 
/// </summary>
public class HorseRaceCheckpointGenerator : IRaceCheckpointGenerator
{
    private int _minExtraCheckpoints;
    private int _maxExtraCheckpoints;
    private float _checkpointVariation;
    private float _strengthVariation;

    /// <summary>
    /// Constructs an instance of the <c>HorseRaceCheckpointGenerator</c> class.
    /// </summary>
    /// <param name="minExtraCheckpoints">The minimum number of checkpoints to generate (does not include the finish line).</param>
    /// <param name="maxExtraCheckpoints">The maximum number of checkpoints to generate (does not include the finish line).</param>
    /// <param name="checkpointVariation">Determines the amount of variation in where the checkpoints are placed. Be wary of turning this number too high, as doing so can result in checkpoints whose positions are out of order.</param>
    /// <param name="strengthVariation">Determines the amount of variation in the ttrength of checkpoints.</param>
    public HorseRaceCheckpointGenerator(int minExtraCheckpoints, int maxExtraCheckpoints, float checkpointVariation, float strengthVariation)
    {
        _minExtraCheckpoints = minExtraCheckpoints;
        _maxExtraCheckpoints = maxExtraCheckpoints;
        _checkpointVariation = checkpointVariation;
        _strengthVariation = strengthVariation;
    }

    public List<HorseRaceCheckpoint> GenerateCheckpoints(int placement)
    {
        List<HorseRaceCheckpoint> checkpoints = new();
        int checkpointCount = Random.Range(_minExtraCheckpoints, _maxExtraCheckpoints + 1);
        float distance = 1f / (checkpointCount + 1f);
        for (var i = 0; i < checkpointCount; i++)
        {
            float thisDistance = distance * (i + 1);
            thisDistance += Random.Range(-_checkpointVariation, _checkpointVariation);
            float strength = 1 + Random.Range(-_strengthVariation, _strengthVariation);
            checkpoints.Add(new HorseRaceCheckpoint(thisDistance, strength));
        }

        float finishLineWeight = 1f;
        //  If the racer in question will be in first place, lower the weight of the finish line to make it move faster towards the end. 
        if (placement == 0)
        {
            finishLineWeight = 0.8f;
        }

        checkpoints.Add(new HorseRaceCheckpoint(1f, finishLineWeight));
        return checkpoints;
    }
}
