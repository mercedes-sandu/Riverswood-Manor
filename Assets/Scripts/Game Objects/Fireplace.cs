using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Fireplace : Interactable
{
    /// <summary>
    /// The fire game object.
    /// </summary>
    [SerializeField] private GameObject fire;
    
    /// <summary>
    /// The note that will become visible when interacting with the fireplace.
    /// </summary>
    [SerializeField] private PaperNote fireplaceNote;

    /// <summary>
    /// The note' invisible display image.
    /// </summary>
    [SerializeField] private Sprite noteDisplayImageVisible;

    /// <summary>
    /// The correct color combination to unlock the secret door.
    /// </summary>
    [SerializeField] private ColoredObject.ColoredObjectColor[] correctColorCombination;
    
    /// <summary>
    /// The list of colors the player has inputted via the buttons above the fireplace.
    /// </summary>
    private List<ColoredObject.ColoredObjectColor> _inputtedColors = new List<ColoredObject.ColoredObjectColor>();
    
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Gets the components and subscribes to GameEvents.
    /// </summary>
    void Start()
    {
        GameEvent.OnColoredButtonPressed += OnColoredButtonPressed;
        
        _audioSource = GetComponent<AudioSource>();
        fire.SetActive(false);
    }
    
    /// <summary>
    /// Changes the cursor to interacting.
    /// </summary>
    public override void OnFocus()
    {
        GameEvent.ChangeCursor(true);
    }
    
    /// <summary>
    /// Plays the noise and pulls up the player's fireplace note, animating it to reveal more text.
    /// </summary>
    public override void OnInteract()
    {
        _audioSource.Play();
        
        fire.SetActive(true);
        GameEvent.ToggleMovement(false, false); // todo: test
        GameEvent.DisplayItemAnimated();
        fireplaceNote.ChangePaperNoteDisplay(noteDisplayImageVisible);
    }
    
    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }

    /// <summary>
    /// Records the new color input and checks if the correct combination has been inputted.
    /// </summary>
    /// <param name="objectColor">The color just inputted by the player.</param>
    private void OnColoredButtonPressed(ColoredObject.ColoredObjectColor objectColor)
    {
        _inputtedColors.Add(objectColor);
        
        if (_inputtedColors.Count < correctColorCombination.Length) return;
        
        while (_inputtedColors.Count > 3) _inputtedColors.RemoveAt(0);
        
        if (correctColorCombination.Where((t, i) => _inputtedColors[i] != t).Any()) return;

        GameEvent.UnlockSecretDoor();
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnColoredButtonPressed -= OnColoredButtonPressed;
    }
}