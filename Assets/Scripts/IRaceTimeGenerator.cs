/// <summary>
/// Classes that implement the <c>IRaceTimeGenerator</c> interface are responsible for generating an array of floats that represent the times it will take for each racer to complete the race.
/// </summary>
public interface IRaceTimeGenerator
{
    /// <summary>
    /// Creates an array of floats that represent the times of each racer.
    /// </summary>
    /// <returns></returns>
    public float[] GenerateRaceTimes(); 
}
