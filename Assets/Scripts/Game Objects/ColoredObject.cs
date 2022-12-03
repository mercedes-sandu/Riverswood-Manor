using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class ColoredObject : Interactable
{
    /// <summary>
    /// The color corresponding to the object.
    /// </summary>
    public enum ColoredObjectColor
    {
        Red, Yellow, Green
    }

    /// <summary>
    /// The object's color.
    /// </summary>
    [SerializeField] private ColoredObjectColor objectColor;
    
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// The animator component.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// The Interacted animator parameter.
    /// </summary>
    private static readonly int Interacted = Animator.StringToHash("Interacted");

    /// <summary>
    /// Gets the components.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _animator.SetBool(Interacted, false);
    }
    
    /// <summary>
    /// Changes the cursor to interacting.
    /// </summary>
    public override void OnFocus()
    {
        GameEvent.ChangeCursor(true);
    }
    
    /// <summary>
    /// Plays the sound clip and animates the object.
    /// </summary>
    public override void OnInteract()
    {
        _audioSource.Play();
        _animator.SetBool(Interacted, true);
    }
    
    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }
    
    /// <summary>
    /// Resets the object animation.
    /// </summary>
    public void ResetAnimation()
    {
        _animator.SetBool(Interacted, false);
    }

    /// <summary>
    /// Gets the object's associated color.
    /// </summary>
    /// <returns>The color.</returns>
    public ColoredObjectColor GetColorOfObject() => objectColor;
}