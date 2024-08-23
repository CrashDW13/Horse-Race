using System;
using UnityEngine;

/// <summary>
/// The <c>HorseRacerDescription</c> class contains cosmetic information about a racer to be displayed in the racer selection UI. 
/// </summary>
[Serializable]
public class HorseRacerDescription
{
    public string HorseName;
    public string HorseOrigin;
    public string JockeyName;
    public string JockeyOrigin;

    /// <summary>
    /// Win percentage is defined as the % of races in which this racer earns 1st place.
    /// </summary>
    [Range(0, 100)] public int WinPercentage;

    /// <summary>
    /// In-the-money percentage is defined as the % of races in which this racer earns at least 3rd place.
    /// </summary>
    [Range(0, 100)] public int InTheMoneyPercentage;

    /// <summary>
    /// The average winning distance is defined as the average distance of a race where the horse wins, measured in furlongs. 
    /// </summary>
    [Range(7,8)]public float AverageWinningDistance;
}
