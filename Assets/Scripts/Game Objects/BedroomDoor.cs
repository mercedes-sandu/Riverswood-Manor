using UnityEngine;

public class BedroomDoor : Interactable
{
    /// <summary>
    /// The door rattle clip.
    /// </summary>
    [SerializeField] private AudioClip doorRattle;
    
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
    /// True if the note has been sent, false otherwise.
    /// </summary>
    private bool _sentNote = false;

    /// <summary>
    /// Gets components.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _paperNoteAnimator = paperNote.GetComponent<Animator>();
        _paperNoteAnimator.SetBool("IsSliding", false);
    }
    
    /// <summary>
    /// Changes the cursor to interacting.
    /// </summary>
    public override void OnFocus()
    {
        GameEvent.ChangeCursor(true);
    }
    
    /// <summary>
    /// Collects and displays the item.
    /// </summary>
    public override void OnInteract()
    {
        _audioSource.PlayOneShot(doorRattle);
    }
    
    /// <summary>
    /// Changes the cursor back to normal and triggers the note to slide in after 3 seconds.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
        if (!_sentNote) Invoke(nameof(SlideNoteIn), 3f);
    }
    
    /// <summary>
    /// Slides the note into view and plays a noise to indicate this.
    /// </summary>
    private void SlideNoteIn()
    {
        _audioSource.PlayOneShot(paperNoise);
        _paperNoteAnimator.SetBool("IsSliding", true);
        _sentNote = true;
    }
}