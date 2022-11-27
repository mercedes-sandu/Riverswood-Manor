using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// Inventory item struct.
    /// </summary>
    public struct InventoryItem
    {
        /// <summary>
        /// The GameObject corresponding to the item.
        /// </summary>
        public GameObject Item;
        
        /// <summary>
        /// The icon of the item in the inventory.
        /// </summary>
        public Sprite Icon;
        
        /// <summary>
        /// The UI display of the item in the inventory.
        /// </summary>
        public Sprite DisplaySprite;
        
        /// <summary>
        /// Initializes an InventoryItem.
        /// </summary>
        /// <param name="item">The item GameObject.</param>
        /// <param name="icon">The item's inventory icon.</param>
        /// <param name="displaySprite">The item's UI display picture.</param>
        public InventoryItem(GameObject item, Sprite icon, Sprite displaySprite)
        {
            Item = item;
            Icon = icon;
            DisplaySprite = displaySprite;
        }
    }
    
    /// <summary>
    /// The player's inventory.
    /// </summary>
    private List<InventoryItem> _inventoryItems = new List<InventoryItem>();

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        GameEvent.OnItemCollect += AddItem;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="icon"></param>
    /// <param name="displaySprite"></param>
    private void AddItem(GameObject item, Sprite icon, Sprite displaySprite)
    {
        _inventoryItems.Add(new InventoryItem(item, icon, displaySprite));
    }
}