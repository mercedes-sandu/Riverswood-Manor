using UnityEngine;

public class Painting : Interactable
{
    /// <summary>
    /// The animator component.
    /// </summary>
    private Animator _animator;
    
    /// <summary>
    /// True if the painting is rotating, false otherwise.
    /// </summary>
    private bool _isRotating;

    /// <summary>
    /// Sets the parent.
    /// </summary>
    void Start()
    {
        _isRotating = false;
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsRotating", false);
    }
    
    /// <summary>
    /// Changes the cursor to interacting.
    /// </summary>
    public override void OnFocus()
    {
        GameEvent.ChangeCursor(true);
    }
    
    /// <summary>
    /// Rotates the painting when clicked.
    /// </summary>
    public override void OnInteract()
    {
        if (!_isRotating && !_animator.GetBool("IsRotating"))
        {
            _isRotating = true;
            _animator.SetBool("IsRotating", true);
        }
    }

    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }

    /// <summary>
    /// Called by the animator when the painting is done rotating.
    /// </summary>
    public void StopRotating()
    {
        _isRotating = false;
        GameEvent.ActivatePaintingPortal();
    }
}