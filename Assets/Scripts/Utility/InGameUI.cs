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
    /// The canvas component.
    /// </summary>
    private Canvas _canvas;

    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Awake()
    {
        GameEvent.OnCursorChange += ChangeCursor;
        GameEvent.OnPlayerToggleMovement += ToggleCursor;
        GameEvent.OnGameStart += ShowCanvas;

        cursorImage.sprite = cursor;
        cursorImage.enabled = true;
        
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
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
    /// Toggles the visibility of the cursor.
    /// </summary>
    /// <param name="canMove">True if the player will be able to move, false otherwise.</param>
    /// <param name="inMenu">True if the player will be in a menu, false otherwise.</param>
    private void ToggleCursor(bool canMove, bool inMenu)
    {
        cursorImage.enabled = canMove;
    }
    
    /// <summary>
    /// Shows the canvas when the game starts.
    /// </summary>
    private void ShowCanvas()
    {
        _canvas.enabled = true;
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnCursorChange -= ChangeCursor;
        GameEvent.OnPlayerToggleMovement -= ToggleCursor;
        GameEvent.OnGameStart -= ShowCanvas;
    }
}