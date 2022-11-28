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
        GameEvent.OnPlayerToggleMovement += ToggleCursor;
        // GameEvent.OnItemDisplay += OnItemDisplay;
        // GameEvent.OnInventoryMenuToggle += ToggleCursorVisibility;
        
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
    /// 
    /// </summary>
    /// <param name="canMove"></param>
    /// <param name="inMenu"></param>
    private void ToggleCursor(bool canMove, bool inMenu)
    {
        cursorImage.enabled = canMove;
        Debug.Log("cursor set to " + cursorImage.enabled);
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnCursorChange -= ChangeCursor;
        // GameEvent.OnInventoryMenuToggle -= ToggleCursorVisibility;
    }
}