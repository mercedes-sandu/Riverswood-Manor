using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class MusicBox : Interactable
{
    /// <summary>
    /// The sound clips corresponding to the colored objects.
    /// </summary>
    [SerializeField] private AudioClip[] soundClips;

    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// The animator component.
    /// </summary>
    private Animator _animator;
    
    /// <summary>
    /// Gets components.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Playing", false);
    }
    
    /// <summary>
    /// Changes the cursor to interacting.
    /// </summary>
    public override void OnFocus()
    {
        GameEvent.ChangeCursor(true);
    }
    
    /// <summary>
    /// Plays the sounds corresponding to the correct order of colored objects and animates the music box.
    /// </summary>
    public override void OnInteract()
    {
        _animator.SetBool("Playing", true);
        StartCoroutine(PlayAudioClipsSequentially());
        _animator.SetBool("Playing", false);
    }
    
    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }

    // todo: test this
    /// <summary>
    /// Plays the sounds corresponding to the correct order of colored objects sequentially.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAudioClipsSequentially()
    {
        yield return null;
        foreach (AudioClip clip in soundClips)
        {
            _audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
    }
}