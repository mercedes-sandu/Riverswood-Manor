using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// The inventory item display image.
    /// </summary>
    [SerializeField] private Image itemImage;

    /// <summary>
    /// The canvas component
    /// </summary>
    private Canvas _canvas;

    /// <summary>
    /// Subscribes to GameEvents and disables UI elements.
    /// </summary>
    void Awake()
    {
        GameEvent.OnItemDisplay += DisplayItem;
        GameEvent.OnInventoryMenuToggle += ToggleInventoryMenu;
        
        _canvas = GetComponent<Canvas>();
        itemImage.enabled = false;
        _canvas.enabled = false;
        
        DontDestroyOnLoad(gameObject);
    }
    
    /// <summary>
    /// Displays the specified item image.
    /// </summary>
    /// <param name="displaySprite">The image of the item to be displayed.</param>
    private void DisplayItem(Sprite displaySprite)
    {
        itemImage.sprite = displaySprite;
        itemImage.enabled = true;
    }

    /// <summary>
    /// Toggles the visibility of the inventory menu.
    /// </summary>
    /// <param name="opening">True if the inventory menu is opening, false otherwise.</param>
    private void ToggleInventoryMenu(bool opening)
    {
        _canvas.enabled = opening;
    }
    
    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnItemDisplay -= DisplayItem;
        GameEvent.OnInventoryMenuToggle -= ToggleInventoryMenu;
    }
}