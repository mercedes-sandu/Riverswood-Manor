using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportWall : Interactable
{
    /// <summary>
    /// The scene the player will teleport to by clicking the wall.
    /// </summary>
    [SerializeField] private string nextSceneName;

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
    /// Changes the scene to the next scene.
    /// </summary>
    public override void OnInteract()
    {
        if (_canClick) SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
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