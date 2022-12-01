using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class SecretExit : MonoBehaviour
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
    
    // todo: remember to make the collider a trigger
    /// <summary>
    /// Ends the game when the player walks through the secret exit.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (!_unlockedSecretDoor || !collision.collider.CompareTag("Player")) return;
        
        _audioSource.Play();
        GameEvent.EndGame();
    }
}