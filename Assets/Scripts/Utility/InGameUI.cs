using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    /// <summary>
    /// The normal cursor sprite.
    /// </summary>
    [SerializeField] private Sprite cursor;

    /// <summary>
    /// The interacting cursor sprite.
    /// </summary>
    [SerializeField] private Sprite cursorInteracting;
    
    /// <summary>
    /// The cursor image.
    /// </summary>
    [SerializeField] private Image cursorImage;

    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Awake()
    {
        GameEvent.OnCursorChange += ChangeCursor;
        GameEvent.OnInventoryMenuToggle += ToggleCursorVisibility;
        
        cursorImage.sprite = cursor;
        cursorImage.enabled = true;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Changes the cursor sprite.
    /// </summary>
    /// <param name="interacting">If true, changes to the interacting sprite. If false, changes to the normal sprite.
    /// </param>
    private void ChangeCursor(bool interacting)
    {
        cursorImage.sprite = interacting ? cursorInteracting : cursor;
    }

    /// <summary>
    /// Sets the cursor to be visible if it is to be locked and invisible otherwise.
    /// </summary>
    /// <param name="toBeLocked">True if the cursor is to be locked, false otherwise.</param>
    private void ToggleCursorVisibility(bool toBeLocked)
    {
        cursorImage.enabled = !toBeLocked;
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnCursorChange -= ChangeCursor;
        GameEvent.OnInventoryMenuToggle -= ToggleCursorVisibility;
    }
}