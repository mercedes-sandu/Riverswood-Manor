using UnityEngine;

public static class GameEvent
{
    /// <summary>
    /// Handles the texture of the mouse cursor.
    /// </summary>
    public delegate void CursorHandler(bool interacting);

    /// <summary>
    /// Handles whether the painting has been clicked
    /// </summary>
    public delegate void PaintingHandler();

    /// <summary>
    /// Handles the player's inventory.
    /// </summary>
    public delegate void InventoryHandler(GameObject item, Sprite icon, Sprite displaySprite);

    /// <summary>
    /// Handles the player's inventory UI.
    /// </summary>
    public delegate void InventoryUIHandler(Sprite displaySprite);
    
    /// <summary>
    /// Handles the inventory menu and cursor.
    /// </summary>
    public delegate void InventoryMenuHandler(bool opening);

    /// <summary>
    /// Detects when the mouse cursor should be changed.
    /// </summary>
    public static event CursorHandler OnCursorChange;

    /// <summary>
    /// Detects when the painting has been clicked
    /// </summary>
    public static event PaintingHandler OnPaintingClick;

    /// <summary>
    /// Detects when an item has been collected.
    /// </summary>
    public static event InventoryHandler OnItemCollect;

    /// <summary>
    /// Detects when an item is to be displayed in the inventory UI.
    /// </summary>
    public static event InventoryUIHandler OnItemDisplay;
    
    /// <summary>
    /// Detects when the inventory opens/closes and the mouse cursor lock state should be changed.
    /// </summary>
    public static event InventoryMenuHandler OnInventoryMenuToggle;

    /// <summary>
    /// Changes the mouse cursor to the specified texture.
    /// </summary>
    /// <param name="interacting">True if we swap to the interacting cursor, false otherwise.</param>
    public static void ChangeCursor(bool interacting) => OnCursorChange?.Invoke(interacting);

    /// <summary>
    /// Activates the painting portal to the next scene.
    /// </summary>
    public static void ActivatePaintingPortal() => OnPaintingClick?.Invoke();

    /// <summary>
    /// Collects the specified item.
    /// </summary>
    /// <param name="item">The item to be collected.</param>
    /// <param name="icon">The icon to be displayed in the inventory.</param>
    /// <param name="displaySprite">The UI element to be displayed upon click in the inventory.</param>
    public static void CollectItem(GameObject item, Sprite icon, Sprite displaySprite) =>
        OnItemCollect?.Invoke(item, icon, displaySprite);
    
    /// <summary>
    /// Displays the specified item in the UI.
    /// </summary>
    /// <param name="displaySprite">The sprite to be displayed.</param>
    public static void DisplayItem(Sprite displaySprite) => OnItemDisplay?.Invoke(displaySprite);
    
    /// <summary>
    /// Toggles the cursor's lock state and inventory menu visibility.
    /// </summary>
    /// <param name="opening">True if the menu is being opened and the cursor should be locked, false otherwise.</param>
    public static void ToggleCursorLock(bool opening) => OnInventoryMenuToggle?.Invoke(opening);
}