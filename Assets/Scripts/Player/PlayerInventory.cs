using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// The player's inventory.
    /// </summary>
    private readonly List<GameObject> _inventoryItems = new List<GameObject>();

    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Awake()
    {
        GameEvent.OnItemCollect += AddItem;
    }

    /// <summary>
    /// Adds an item to the player's inventory.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="icon">The item's icon.</param>
    /// <param name="displaySprite">The item's display sprite.</param>
    private void AddItem(GameObject item, Sprite icon, Sprite displaySprite)
    {
        if (!_inventoryItems.Contains(item))
        {
            _inventoryItems.Add(item);
        }
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnItemCollect -= AddItem;
    }
}