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
        GameEvent.CollectItem(gameObject, paperNoteIcon, paperNoteDisplay);
        GameEvent.DisplayItem(paperNoteDisplay);
    }
    
    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }
}