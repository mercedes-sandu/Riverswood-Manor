using UnityEngine;

public class BedroomScene : MonoBehaviour
{
    /// <summary>
    /// The scream audio clip.
    /// </summary>
    [SerializeField] private AudioClip countessScream;

    /// <summary>
    /// The paper audio clip.
    /// </summary>
    [SerializeField] private AudioClip paperNoise;
    
    /// <summary>
    /// The paper note that slides from underneath the door.
    /// </summary>
    [SerializeField] private GameObject paperNote;

    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// The paper note animator component.
    /// </summary>
    private Animator _paperNoteAnimator;

    /// <summary>
    /// Gets components and invokes methods.
    /// </summary>
    void Start()
    {
        _paperNoteAnimator = paperNote.GetComponent<Animator>();
        _paperNoteAnimator.SetBool("IsSliding", false);
        _audioSource = GetComponent<AudioSource>();
        Invoke(nameof(PlayScream), 1.0f);
        Invoke(nameof(SlideNoteIn), countessScream.length + 1.0f);
    }

    /// <summary>
    /// Plays the scream audio clip.
    /// </summary>
    private void PlayScream()
    {
        _audioSource.PlayOneShot(countessScream);
    }

    /// <summary>
    /// Slides the note into view and plays a noise to indicate this.
    /// </summary>
    private void SlideNoteIn()
    {
        _audioSource.PlayOneShot(paperNoise);
        _paperNoteAnimator.SetBool("IsSliding", true);
    }
}