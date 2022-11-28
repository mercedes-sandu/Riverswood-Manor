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
    /// The item button prefab.
    /// </summary>
    [SerializeField] private GameObject itemButtonPrefab;

    /// <summary>
    /// The item image close button.
    /// </summary>
    [SerializeField] private GameObject itemImageCloseButton;
    
    /// <summary>
    /// The player's close key.
    /// </summary>
    [SerializeField] private KeyCode closeKey = KeyCode.Escape;

    /// <summary>
    /// The canvas component
    /// </summary>
    private Canvas _canvas;

    /// <summary>
    /// Subscribes to GameEvents and disables UI elements.
    /// </summary>
    void Awake()
    {
        GameEvent.OnItemCollect += AddItem;
        GameEvent.OnItemDisplay += DisplayItem;
        GameEvent.OnInventoryMenuToggle += ToggleInventoryMenu;
        
        _canvas = GetComponent<Canvas>();
        itemImage.enabled = false;
        itemImageCloseButton.SetActive(false);
        _canvas.enabled = false;
        
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(closeKey))
        {
            if (itemImage.enabled) CloseItemDisplay();
        }
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
        // itemButton.transform.SetParent(inventoryPanel.transform);
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
    /// 
    /// </summary>
    public void CloseItemDisplay()
    {
        itemImage.enabled = false;
        itemImageCloseButton.SetActive(false);

        if (inventoryPanel.activeSelf)
        {
            Debug.Log("closed item display in inventory");
            GameEvent.ToggleMovement(false, true);
        }
        else
        {
            Debug.Log("closed solo item display");
            GameEvent.ToggleMovement(true, false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="displaySprite"></param>
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