using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// The <c>HorseRacerData</c> class serves as a ScriptableObject container for data regarding information about the racers that won't change at runtime, such as names, descriptions, and graphics. 
/// </summary>
[Serializable, CreateAssetMenu(fileName = "New Racer", menuName = "HorseRacing/Racer Data")]
public class HorseRacerData : ScriptableObject
{
    /// <summary>
    /// See class description. Contains cosmetic information regarding a racer's stats.
    /// </summary>
    [SerializeField] public HorseRacerDescription Description;

    /// <summary>
    /// See class description. Contains information regarding a racer's graphics. 
    /// </summary>
    [SerializeField] public HorseRacerGraphicsSprite Graphics;

    /// <summary>
    /// Creates an instance of the <c>HorseRacer</c> class by getting a reference to a GameObject in the scene. Assumes the default weight of a racer to be 1.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public HorseRacer CreateRacer(ref GameObject gameObject)
    {
        return new HorseRacer(this, gameObject, 1);
    }
}
