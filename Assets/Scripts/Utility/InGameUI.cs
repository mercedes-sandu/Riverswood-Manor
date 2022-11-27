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
        
        cursorImage.sprite = cursor;
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
}