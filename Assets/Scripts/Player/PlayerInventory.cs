using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// The player's inventory.
    /// </summary>
    private readonly List<GameObject> _inventoryItems = new List<GameObject>();

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
        if (!_inventoryItems.Contains(item))
        {
            _inventoryItems.Add(item);
        }
    }
}