using UnityEngine;

public class BedroomScene : MonoBehaviour
{
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Awake()
    {
        GameEvent.OnGameStart += OnGameStart;
    }
    
    /// <summary>
    /// Gets components.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Invokes the scream one second after starting the game.
    /// </summary>
    private void OnGameStart()
    {
        Invoke(nameof(PlayScream), 1.0f);
    }
    
    /// <summary>
    /// Plays the scream audio clip.
    /// </summary>
    private void PlayScream()
    {
        _audioSource.Play();
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnGameStart -= OnGameStart;
    }
}