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
    /// The box collider component.
    /// </summary>
    private BoxCollider _boxCollider;

    /// <summary>
    /// The SteppingOn animator parameter.
    /// </summary>
    private static readonly int SteppingOn = Animator.StringToHash("SteppingOn");

    /// <summary>
    /// Sets the animator and gets the audio source.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = true;
        noteAnimator.SetBool(SteppingOn, false);
    }

    public void RemoveCollider()
    {
        _boxCollider.enabled = false;
    }

    /// <summary>
    /// If the player steps on this part of the rug, the sound will play and the note will become visible.
    /// </summary>
    /// <param name="other">The collider with this part of the rug.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _audioSource.Play();
        noteAnimator.SetBool(SteppingOn, true);
    }

    /// <summary>
    /// If the player stops stepping on this part of the rug, the sound will play and the note will become invisible.
    /// </summary>
    /// <param name="other">The collider with this part of the rug.</param>
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _audioSource.Play();
        noteAnimator.SetBool(SteppingOn, false);
    }
}