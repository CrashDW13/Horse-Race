using System.Collections.Generic;

/// <summary>
/// Classes that implement the <c>IRaceCheckpointGenerator</c> interface are responsible for generating a list of checkpoints that will determine the specifics of a racer's movement through their race.
/// </summary>
public interface IRaceCheckpointGenerator
{
    /// <summary>
    /// Generates a list of checkpoints that determine the specifics of a racer's movement through their race.
    /// </summary>
    /// <param name="index">The predetermined placement of the racer.</param>
    /// <returns></returns>
    public List<HorseRaceCheckpoint> GenerateCheckpoints(int index);
}
