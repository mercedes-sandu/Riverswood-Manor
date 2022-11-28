using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    /// <summary>
    /// The button icon image.
    /// </summary>
    [SerializeField] private Image iconImage;
    
    /// <summary>
    /// The item.
    /// </summary>
    private GameObject _item;
    
    /// <summary>
    /// The item icon.
    /// </summary>
    private Sprite _icon;
    
    /// <summary>
    /// The item display image.
    /// </summary>
    private Sprite _displayImage;

    /// <summary>
    /// The inventory UI.
    /// </summary>
    private InventoryUI _inventoryUI;

    /// <summary>
    /// Sets the components.
    /// </summary>
    void Start()
    {
        _inventoryUI = transform.parent.transform.parent.GetComponent<InventoryUI>();
    }
    
    /// <summary>
    /// Sets the item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="icon"></param>
    /// <param name="displayImage"></param>
    public void SetItem(GameObject item, Sprite icon, Sprite displayImage)
    {
        _item = item;
        _icon = icon;
        iconImage.sprite = _icon;
        _displayImage = displayImage;
    }
    
    /// <summary>
    /// Displays the item.
    /// </summary>
    public void DisplayItem()
    {
        _inventoryUI.DisplayItem(_displayImage);
    }
}