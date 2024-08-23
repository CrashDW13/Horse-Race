using System;

/// <summary>
/// The <c>HorseRaceCheckpoint</c> class represents a point in space that a racer will travel to. 
/// </summary>
public class HorseRaceCheckpoint
{
    //  Checkpoints are determined by placement and weight.
    //  DistancePercentage: How far along the race in this checkpoint going to go into effect? (0-1 scale, representing %)
    //  Weight: How much of the racer's time is going to be spent here relative to the time it'll take to reach the other checkpoints? (default 1)

    //  Example: A racer is destined to take 10 seconds to reach the finish line. A checkpoint is placed 50% of the way through the race, with a weight of 3. A checkpoint is also automatically placed at the finish line (100%) with a weight of 1. 
    //  In this case, 75% (3/4) of the racer's time would be spent in the first half, and 25% (1/4) would be spent in the second half. 

    //  Example: A racer is destined to take 10 seconds to reach the finish line. Three checkpoints are placed at the 25%, 50%, and 75% marks, with weights of 1, 2, and 3 respectively.
    //  Start -> Checkpoint 1: (1/7): 1.43s (would be 2.5s)
    //  Checkpoint 1 -> Checkpoint 2: (2/7): 2.86s (would be 2.5s)
    //  Checkpoint 2 -> Checkpoint 3: (3/7): 4.29s (would be 2.5s)
    //  Checkpoint 3 -> Finish Line: (1/7): 1.43s (would be 2.5s)


    private float _distancePercentage;
    public float DistancePercentage { get { return _distancePercentage; } }
    private float _weight;
    public float Weight { get { return _weight; } }

    public HorseRaceCheckpoint(float distancePercentage, float weight)
    {
        _distancePercentage = distancePercentage;
        _weight = weight;
    }
}