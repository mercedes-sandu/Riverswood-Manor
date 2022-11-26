using UnityEngine;

public class Interactor : MonoBehaviour
{
    /// <summary>
    /// The transform of the player's interaction point.
    /// </summary>
    [SerializeField] private Transform interactionPoint;
    
    /// <summary>
    /// The radius around the interaction point that will be checked for interactable objects.
    /// </summary>
    [SerializeField] private float interactionPointRadius = 0.34f;
    
    /// <summary>
    /// The layer(s) that interactable objects are on.
    /// </summary>
    [SerializeField] private LayerMask interactableMask;

    /// <summary>
    /// The list of interactable objects the player can access.
    /// </summary>
    private readonly Collider[] _colliders = new Collider[3];
    
    /// <summary>
    /// The number of interactable objects found.
    /// </summary>
    [SerializeField] private int numFound;

    /// <summary>
    /// Sets the cursor to be normal.
    /// </summary>
    void Start()
    {
        GameEvent.ChangeCursor(false);
    }
    
    /// <summary>
    /// Consistently checks for interactable objects.
    /// </summary>
    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, 
            _colliders, interactableMask);

        if (numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            if (interactable != null)
            {
                GameEvent.ChangeCursor(true);
                
                if (Input.GetMouseButtonDown(0))
                {
                    interactable.Interact(this);
                }
            }
        }
        else
        {
            GameEvent.ChangeCursor(false);
        }
    }

    /// <summary>
    /// Draws the interaction radius in the editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}