using UnityEngine;

public class BedroomScene : MonoBehaviour
{
    /// <summary>
    /// The scream audio clip.
    /// </summary>
    [SerializeField] private AudioClip countessScream;

    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Gets components and invokes methods.
    /// </summary>
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Invoke(nameof(PlayScream), 1.0f);
    }

    /// <summary>
    /// Plays the scream audio clip.
    /// </summary>
    private void PlayScream()
    {
        _audioSource.PlayOneShot(countessScream);
    }
}