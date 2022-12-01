using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ColoredButton : Interactable
{
    /// <summary>
    /// The button's color.
    /// </summary>
    [SerializeField] private ColoredObject.ColoredObjectColor buttonColor;
    
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Gets components.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// Changes the cursor to interacting.
    /// </summary>
    public override void OnFocus()
    {
        GameEvent.ChangeCursor(true);
    }
    
    /// <summary>
    /// Plays the sound and inputs the player's selected color via the colored button press event.
    /// </summary>
    public override void OnInteract()
    {
        _audioSource.Play();
        GameEvent.PressColoredButton(buttonColor);
    }
    
    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }
}