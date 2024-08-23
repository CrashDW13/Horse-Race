/// <summary>
/// Classes that implement the <c>IRaceWeightedPlacement</c> interface are responsible for determining the placements of a race.
/// </summary>
public interface IRaceWeightedPlacement
{
    /// <summary>
    /// Creates an array of ints whose values correspond with racers and order correspond with placements. (ie. (4, 0, 1, 3) means that Racer 4 finishes first, Racer 0 finishes second, etc.) 
    /// </summary>
    /// <returns></returns>
    public int[] GetPlacements();
}