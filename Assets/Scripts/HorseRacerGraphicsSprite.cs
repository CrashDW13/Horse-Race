using System;
using UnityEngine;

/// <summary>
/// The <c>HorseRacerGraphicsSprite</c> class implements an instance of <c>IHorseRacerGraphics</c> that uses a SpriteRenderer and a color-mapped shading material to change colors.
/// </summary>
[Serializable]
public class HorseRacerGraphicsSprite : IHorseRacerGraphics
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private RuntimeAnimatorController _controller;
    [SerializeField] private Material _material;
    [SerializeField] private Color _racerColor;
    [SerializeField] private Color _horseColor;

    /// <summary>
    /// The color of the racer's armor.
    /// </summary>
    public Color RacerColor { get => _racerColor; }

    /// <summary>
    /// The color of the racer's horse.
    /// </summary>
    public Color HorseColor { get => _horseColor; } 

    /// <summary>
    /// The animator attached to the <c>GameObject</c> that represents the racer.
    /// </summary>
    public Animator RacerAnimator { get; private set; }


    public void Setup(ref GameObject gameObject)
    {
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = _sprite;

        Material material = new Material(_material);
        material.SetColor("_RacerColor", _racerColor);
        material.SetColor("_HorseColor", _horseColor);
        renderer.material = material;

        Animator animator = gameObject.AddComponent<Animator>();
        RacerAnimator = animator;
        animator.runtimeAnimatorController = _controller;
        animator.Play("Mount");
    }
}
