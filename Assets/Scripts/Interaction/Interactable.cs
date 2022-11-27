using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    /// <summary>
    /// Assigns the interactable layer to this object.
    /// </summary>
    public virtual void Awake()
    {
        gameObject.layer = 7;
    }

    /// <summary>
    /// Called when the player starts focusing on this object.
    /// </summary>
    public abstract void OnFocus();
    
    /// <summary>
    /// Called when the player interacts with this object.
    /// </summary>
    public abstract void OnInteract();
    
    /// <summary>
    /// Called when the player stops focusing on this object.
    /// </summary>
    public abstract void OnLoseFocus();
}