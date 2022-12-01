using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class BookCase : MonoBehaviour
{
    /// <summary>
    /// The animator component.
    /// </summary>
    private Animator _animator;
    
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;
    
    /// <summary>
    /// Subscribes to GameEvents and sets components.
    /// </summary>
    void Start()
    {
        GameEvent.OnSecretDoorUnlocked += MoveBookcase;
        
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _animator.SetBool("Unlocked", false);
    }

    /// <summary>
    /// Moves the bookcase to reveal the secret exit.
    /// </summary>
    private void MoveBookcase()
    {
        _audioSource.Play();
        _animator.SetBool("Unlocked", true);
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnSecretDoorUnlocked -= MoveBookcase;
    }
}