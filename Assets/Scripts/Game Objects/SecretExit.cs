using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class SecretExit : Interactable
{
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;
    
    /// <summary>
    /// True if the secret door has been unlocked (revealing this exit), false otherwise.
    /// </summary>
    private bool _unlockedSecretDoor = false;
    
    /// <summary>
    /// Gets components and subscribes to GameEvents.
    /// </summary>
    void Start()
    {
        GameEvent.OnSecretDoorUnlocked += OnSecretDoorUnlocked;
        
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Sets the secret door to unlocked.
    /// </summary>
    private void OnSecretDoorUnlocked()
    {
        _unlockedSecretDoor = true;
    }
    
    /// <summary>
    /// Changes the cursor to interacting.
    /// </summary>
    public override void OnFocus()
    {
        GameEvent.ChangeCursor(true);
    }
    
    /// <summary>
    /// Ends the game when the player clicks on the secret exit.
    /// </summary>
    public override void OnInteract()
    {
        if (!_unlockedSecretDoor) return;
        
        _audioSource.Play();
        GameEvent.EndGame();
    }

    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }

    /// <summary>
    /// Ends the game when the player walks through the secret exit.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!_unlockedSecretDoor || !other.CompareTag("Player")) return;
        
        _audioSource.Play();
        GameEvent.EndGame();
    }
}