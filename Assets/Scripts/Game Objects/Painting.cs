using System.Collections;
using UnityEngine;

public class Painting : Interactable
{
    // /// <summary>
    // /// 
    // /// The rotation that the painting will go to when interacted with.
    // /// </summary>
    // [SerializeField] private Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
    //
    // /// <summary>
    // /// The time it takes to rotate the painting.
    // /// </summary>
    // [SerializeField] private float timeToRotate = 0.5f;

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
        if (!_isRotating)
        {
            _isRotating = true;
            _animator.SetBool("IsRotating", true);
        }
        
        // if (!_isRotating) StartCoroutine(RotatePainting());
    }

    /// <summary>
    /// Changes the cursor back to normal.
    /// </summary>
    public override void OnLoseFocus()
    {
        GameEvent.ChangeCursor(false);
    }

    // /// <summary>
    // /// Gradually rotates the painting.
    // /// </summary>
    // /// <returns></returns>
    // private IEnumerator RotatePainting()
    // {
    //     float timeElapsed = 0;
    //     Quaternion currentRotation = _parent.rotation;
    //     _isRotating = true;
    //
    //     while (timeElapsed < timeToRotate)
    //     {
    //         _parent.rotation = Quaternion.Lerp(currentRotation, targetRotation, timeElapsed / timeToRotate);
    //         timeElapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     
    //     _parent.rotation = targetRotation;
    //     _isRotating = false;
    // }

    public void StopRotating()
    {
        _isRotating = false;
    }
}