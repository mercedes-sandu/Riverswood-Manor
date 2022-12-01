using UnityEngine;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(AudioSource))]
public class EndScreen : MonoBehaviour
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
    /// Subscribes to GameEvents and gets the components.
    /// </summary>
    void Start()
    {
        GameEvent.OnGameEnd += OnGameEnd;
        
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    /// <summary>
    /// Displays the canvas and plays the music.
    /// </summary>
    private void OnGameEnd()
    {
        GameEvent.ToggleMovement(false, false); // todo: test
        _canvas.enabled = true;
        _audioSource.Play();
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnGameEnd -= OnGameEnd;
    }
}