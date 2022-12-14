using UnityEngine;

public class PaperNote : Interactable
{
    /// <summary>
    /// The paper note inventory icon.
    /// </summary>
    [SerializeField] private Sprite paperNoteIcon;

    /// <summary>
    /// The paper note UI display image.
    /// </summary>
    [SerializeField] private Sprite paperNoteDisplay;

    /// <summary>
    /// True if the note comes from under the rug, false otherwise.
    /// </summary>
    [SerializeField] private bool isRugNote = false;

    /// <summary>
    /// The rug section covering the rug note.
    /// </summary>
    [SerializeField] private RugSection rugSection;
    
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
    /// Collects and displays the item.
    /// </summary>
    public override void OnInteract()
    {
        _audioSource.Play();
        GameEvent.CollectItem(gameObject, paperNoteIcon, paperNoteDisplay);
        GameEvent.DisplayItem(paperNoteDisplay);
        if (isRugNote) rugSection.RemoveCollider();
        gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }
    
    /// <summary>
    /// Changes the paper note display to the new specified sprite.
    /// </summary>
    /// <param name="newDisplay">The new display image.</param>
    public void ChangePaperNoteDisplay(Sprite newDisplay)
    {
        paperNoteIcon = newDisplay;
        paperNoteDisplay = newDisplay;
    }
}