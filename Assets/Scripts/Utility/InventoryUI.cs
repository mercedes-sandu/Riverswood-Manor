using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// The inventory UI panel.
    /// </summary>
    [SerializeField] private GameObject panel;
    
    /// <summary>
    /// The inventory panel.
    /// </summary>
    [SerializeField] private GameObject inventoryPanel;

    /// <summary>
    /// The close button.
    /// </summary>
    [SerializeField] private GameObject closeButton;
    
    /// <summary>
    /// The inventory item display image.
    /// </summary>
    [SerializeField] private Image itemImage;

    /// <summary>
    /// The inventory item animated display image.
    /// </summary>
    [SerializeField] private Image animatedItemImage;

    /// <summary>
    /// The item button prefab.
    /// </summary>
    [SerializeField] private GameObject itemButtonPrefab;

    /// <summary>
    /// The item image close button.
    /// </summary>
    [SerializeField] private GameObject itemImageCloseButton;
    
    /// <summary>
    /// The animated item image close button.
    /// </summary>
    [SerializeField] private GameObject animatedItemImageCloseButton;
    
    /// <summary>
    /// The player's close key.
    /// </summary>
    [SerializeField] private KeyCode closeKey = KeyCode.Escape;

    /// <summary>
    /// The canvas component
    /// </summary>
    private Canvas _canvas;

    /// <summary>
    /// The item image animator component.
    /// </summary>
    private Animator _itemImageAnimator;

    /// <summary>
    /// Subscribes to GameEvents and disables UI elements.
    /// </summary>
    void Awake()
    {
        GameEvent.OnItemCollect += AddItem;
        GameEvent.OnItemDisplay += DisplayItem;
        GameEvent.OnItemDisplayAnimated += DisplayAnimatedItem;
        GameEvent.OnInventoryMenuToggle += ToggleInventoryMenu;
        
        _canvas = GetComponent<Canvas>();
        _itemImageAnimator = animatedItemImage.GetComponent<Animator>();
        itemImage.enabled = false;
        animatedItemImage.enabled = false;
        itemImageCloseButton.SetActive(false);
        animatedItemImageCloseButton.SetActive(false);
        _canvas.enabled = false;
    }

    /// <summary>
    /// Checks to close the item display.
    /// </summary>
    void Update()
    {
        if (!Input.GetKeyDown(closeKey)) return;
        
        if (itemImage.enabled) CloseItemDisplay();
        if (animatedItemImage.enabled) CloseAnimatedItemDisplay();
    }
    
    /// <summary>
    /// Adds the item to the inventory.
    /// </summary>
    /// <param name="item">The item to be added.</param>
    /// <param name="icon">The icon of the item to be added</param>
    /// <param name="displaySprite">The display sprite of the item to be added.</param>
    private void AddItem(GameObject item, Sprite icon, Sprite displaySprite)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, inventoryPanel.transform);
        itemButton.GetComponent<ItemButton>().SetItem(item, icon, displaySprite);
    }
    
    /// <summary>
    /// Displays the specified item image.
    /// </summary>
    /// <param name="displaySprite">The image of the item to be displayed.</param>
    public void DisplayItem(Sprite displaySprite)
    {
        itemImageCloseButton.SetActive(true);

        if (_canvas.enabled)
        {
            itemImage.sprite = displaySprite;
            itemImage.enabled = true;
        }
        else
        {
            ShowOnlyItemDisplay(displaySprite);
        }
    }

    /// <summary>
    /// Displays the animated item image.
    /// </summary>
    private void DisplayAnimatedItem()
    {
        _canvas.enabled = true;
        animatedItemImage.enabled = true;
        animatedItemImageCloseButton.SetActive(true);
        _itemImageAnimator.Play("InventoryImageChange");
    }

    /// <summary>
    /// Toggles the visibility of the inventory menu.
    /// </summary>
    /// <param name="opening">True if the inventory menu is opening, false otherwise.</param>
    private void ToggleInventoryMenu(bool opening)
    {
        panel.SetActive(opening);
        inventoryPanel.SetActive(opening);
        closeButton.SetActive(opening);
        _canvas.enabled = opening;
    }

    /// <summary>
    /// Closes the player's inventory.
    /// </summary>
    public void CloseInventory()
    {
        _canvas.enabled = false;
        GameEvent.ToggleMovement(true, false);
    }
    
    /// <summary>
    /// Closes the item display.
    /// </summary>
    public void CloseItemDisplay()
    {
        itemImage.enabled = false;
        itemImageCloseButton.SetActive(false);
        if (!inventoryPanel.activeSelf) _canvas.enabled = false;
        GameEvent.ToggleMovement(!inventoryPanel.activeSelf, inventoryPanel.activeSelf);
    }

    /// <summary>
    /// Closes the animated item display.
    /// </summary>
    public void CloseAnimatedItemDisplay()
    {
        animatedItemImage.enabled = false;
        animatedItemImageCloseButton.SetActive(false);
        if (!inventoryPanel.activeSelf) _canvas.enabled = false;
        GameEvent.ToggleMovement(!inventoryPanel.activeSelf, inventoryPanel.activeSelf);
    }

    /// <summary>
    /// Shows the item display exclusively (no inventory menu).
    /// </summary>
    /// <param name="displaySprite">The displayed sprite.</param>
    private void ShowOnlyItemDisplay(Sprite displaySprite)
    {
        GameEvent.ToggleMovement(false, false);
        
        _canvas.enabled = true;
        panel.SetActive(false);
        inventoryPanel.SetActive(false);
        closeButton.SetActive(false);
        itemImage.sprite = displaySprite;
        itemImage.enabled = true;
    }
    
    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnItemCollect -= AddItem;
        GameEvent.OnItemDisplay -= DisplayItem;
        GameEvent.OnInventoryMenuToggle -= ToggleInventoryMenu;
    }
}