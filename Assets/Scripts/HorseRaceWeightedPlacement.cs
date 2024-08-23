using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates an array of ints that determine the racers' placement based on a basic weight picking algorithm. 
/// </summary>
public class HorseRaceWeightedPlacement : IRaceWeightedPlacement
{
    private List<HorseRacer> _racers;
    public HorseRaceWeightedPlacement(List<HorseRacer> racers)
    {
        _racers = racers;
    }

    public int[] GetPlacements()
    {
        List<HorseRacer> tempRacers = new List<HorseRacer>(_racers);
        int[] placements = new int[_racers.Count];
        int totalWeights = 0;
        foreach (HorseRacer racer in _racers)
        {
            totalWeights += racer.Weight;
        }

        for (var i = 0; i < placements.Length; i++)
        {
            HorseRacer horseRacer = GetWeightedOutput(tempRacers, totalWeights);
            placements[i] = _racers.FindIndex(x => x == horseRacer);
            totalWeights -= horseRacer.Weight;
            tempRacers.Remove(horseRacer);
        }
        return placements;
    }

    private HorseRacer GetWeightedOutput(List<HorseRacer> racers, int totalWeight)
    {
        HorseRacer result = null;

        int randomWeightValue = Random.Range(1, totalWeight + 1);
        int thisWeight = 0;
        foreach(HorseRacer racer in racers)
        {
            thisWeight += racer.Weight;
            if (randomWeightValue <= thisWeight)
            {
                result = racer;
                break;
            }
        }
        return result;
    }
}
