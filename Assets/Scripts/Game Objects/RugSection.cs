using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class RugSection : MonoBehaviour
{
    /// <summary>
    /// The animator component of the note that is hiding under the rug.
    /// </summary>
    [SerializeField] private Animator noteAnimator;

    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Sets the animator and gets the audio source.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        noteAnimator.SetBool("SteppingOn", false);
    }

    /// <summary>
    /// If the player steps on this part of the rug, the sound will play and the note will become visible.
    /// </summary>
    /// <param name="collision">The collision with this part of the rug.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;
        
        _audioSource.Play();
        noteAnimator.SetBool("SteppingOn", true);
    }

    /// <summary>
    /// If the player stops stepping on this part of the rug, the sound will play and the note will become invisible.
    /// </summary>
    /// <param name="other">The collision with this part of the rug.</param>
    private void OnCollisionExit(Collision other)
    {
        if (!other.collider.CompareTag("Player")) return;
        
        _audioSource.Play();
        noteAnimator.SetBool("SteppingOn", false);
    }
}