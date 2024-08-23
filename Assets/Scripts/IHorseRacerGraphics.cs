using UnityEngine;
/// <summary>
/// Classes that implement the <c>IHorseRacerGraphics</c> interface are responsible for setting up the appearance of each racer. 
/// </summary>
public interface IHorseRacerGraphics
{
    /// <summary>
    /// Sets up a racer's graphical appearance.
    /// </summary>
    /// <param name="gameObject">The <c>GameObject</c> whose appearance should be edited.</param>
    public void Setup(ref GameObject gameObject);
}
