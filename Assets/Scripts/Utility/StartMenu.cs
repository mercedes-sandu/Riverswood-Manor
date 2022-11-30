using UnityEngine;

public class StartMenu : MonoBehaviour
{
    /// <summary>
    /// The canvas component.
    /// </summary>
    private Canvas _canvas;
    
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Sets components and enables the cursor.
    /// </summary>
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _audioSource = GetComponent<AudioSource>();
        FirstPersonController.Instance.CanMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        _audioSource.Stop();
        _canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FirstPersonController.Instance.CanMove = true;
        GameEvent.StartGame();
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}