using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>HorseRacerSequence</c> class creates a DOTween sequence based on a given list of checkpoints.
/// </summary>
public class HorseRacerSequence
{
    /// <summary>
    /// Generates a DOTween sequence based on a list of checkpoints.
    /// </summary>
    /// <param name="target">The GameObject that this sequence will affect.</param>
    /// <param name="checkpoints">A list of checkpoints to generate the sequence from.</param>
    /// <param name="startingPosition">The starting position of the race.</param>
    /// <param name="totalDistance">The total distance the race will travel.</param>
    /// <param name="totalTime">The total time this racer will spend in the race.</param>
    /// <returns>A DOTween sequence representing how the racer will move through the race.</returns>
    public Sequence GenerateSequence(GameObject target, List<HorseRaceCheckpoint> checkpoints, Vector3 startingPosition, float totalDistance, float totalTime)
    {
        Sequence sequence = DOTween.Sequence(target);

        float totalWeights = GetTotalCheckpointWeights(checkpoints);
        foreach (HorseRaceCheckpoint checkpoint in checkpoints)
        {
            float distance = totalDistance * checkpoint.DistancePercentage;
            Debug.Log(startingPosition.x + distance);
            sequence.Append(target.transform.DOMoveX(startingPosition.x + distance, totalTime * (checkpoint.Weight / totalWeights)).SetEase(Ease.Linear));
        }

        sequence.Pause();
        return sequence; 
    }

    private float GetTotalCheckpointWeights(List<HorseRaceCheckpoint> checkpoints)
    {
        float count = 0;
        foreach (HorseRaceCheckpoint checkpoint in checkpoints)
        {
            count += checkpoint.Weight;
        }
        return count;
    }
}
