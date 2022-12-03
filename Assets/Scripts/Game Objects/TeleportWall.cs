using UnityEngine;

public class TeleportWall : Interactable
{
    /// <summary>
    /// The teleport location.
    /// </summary>
    [SerializeField] private Transform teleportPoint;

    /// <summary>
    /// True if the player has moved the painting, false otherwise.
    /// </summary>
    private bool _canClick;

    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Start()
    {
        GameEvent.OnPaintingClick += SetClickability;
        _canClick = false;
    }
    
    /// <summary>
    /// Changes the cursor to interacting.
    /// </summary>
    public override void OnFocus()
    {
        GameEvent.ChangeCursor(true);
    }

    /// <summary>
    /// Teleports the player to the next location.
    /// </summary>
    public override void OnInteract()
    {
        if (_canClick) GameEvent.TeleportPlayer(teleportPoint);
    }

    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }

    /// <summary>
    /// Allows the wall to be clicked.
    /// </summary>
    private void SetClickability()
    {
        _canClick = true;
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnPaintingClick -= SetClickability;
    }
}