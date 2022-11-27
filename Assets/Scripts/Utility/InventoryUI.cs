using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// The inventory item display image.
    /// </summary>
    [SerializeField] private Image itemImage;

    /// <summary>
    /// Subscribes to GameEvents and disables UI elements.
    /// </summary>
    void Awake()
    {
        GameEvent.OnItemDisplay += DisplayItem;
        
        itemImage.enabled = false;
        
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
}