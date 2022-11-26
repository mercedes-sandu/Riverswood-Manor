using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The orientation of the player.
    /// </summary>
    [SerializeField] private Transform orientation;
    
    /// <summary>
    /// The movement speed of the player.
    /// </summary>
    [SerializeField] private float moveSpeed;

    /// <summary>
    /// The jump force of the player.
    /// </summary>
    [SerializeField] private float jumpForce;

    /// <summary>
    /// The jump cooldown of the player.
    /// </summary>
    [SerializeField] private float jumpCooldown;
    
    /// <summary>
    /// The air multiplier.
    /// </summary>
    [SerializeField] private float airMultiplier;

    /// <summary>
    /// The height of the player.
    /// </summary>
    [SerializeField] private float playerHeight;

    /// <summary>
    /// The offset to use for ground checking.
    /// </summary>
    [SerializeField] private float groundOffset;

    /// <summary>
    /// The level of drag on the ground.
    /// </summary>
    [SerializeField] private float groundDrag;

    /// <summary>
    /// The layer mask that shows what is the ground.
    /// </summary>
    [SerializeField] private LayerMask groundMask;

    /// <summary>
    /// The player's horizontal input.
    /// </summary>
    private float _horizontalInput;
    
    /// <summary>
    /// The player's vertical input.
    /// </summary>
    private float _verticalInput;
    
    /// <summary>
    /// The player's direction of movement.
    /// </summary>
    private Vector3 _moveDirection;
    
    /// <summary>
    /// True if the player is grounded, false otherwise.
    /// </summary>
    private bool _grounded;

    /// <summary>
    /// True if the player is ready to jump, false otherwise.
    /// </summary>
    private bool _readyToJump;

    /// <summary>
    /// The player's Rigidbody component.
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// Get the Rigidbody and freeze its rotation.
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _readyToJump = true;
    }
    
    /// <summary>
    /// Updates the player's input.
    /// </summary>
    void Update()
    {
        // updated whether the player is grounded
        _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 
                                                                      groundOffset, groundMask);
        
        // get horizontal and vertical input
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        
        // checks for jumping
        if (Input.GetKey(KeyCode.Space) && _readyToJump && _grounded)
        {
            _readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // limit the player's speed, if necessary
        Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
        }
        
        // apply drag, if necessary
        _rb.drag = _grounded ? groundDrag : 0;
    }

    /// <summary>
    /// Moves the player.
    /// </summary>
    void FixedUpdate()
    {
        // calculate movement direction
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        
        _rb.AddForce(_grounded ? _moveDirection.normalized * moveSpeed * 10f : _moveDirection.normalized * moveSpeed * 
                                                                               10f * airMultiplier, ForceMode.Force);
    }

    /// <summary>
    /// Makes the player jump.
    /// </summary>
    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Resets the player's ready to jump status.
    /// </summary>
    private void ResetJump()
    {
        _readyToJump = true;
    }
}