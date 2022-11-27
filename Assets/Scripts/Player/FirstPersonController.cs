using System.Collections;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    /// <summary>
    /// True if the player can move, false otherwise.
    /// </summary>
    public bool CanMove { get; private set; } = true;

    /// <summary>
    /// True if the player should jump, false otherwise.
    /// </summary>
    private bool ShouldJump => Input.GetKey(jumpKey) && _characterController.isGrounded && _readyToJump;

    /// <summary>
    /// True if the player should crouch, false otherwise.
    /// </summary>
    private bool ShouldCrouch =>
        Input.GetKeyDown(crouchKey) && !_duringCrouchAnimation && _characterController.isGrounded;

    /// <summary>
    /// The player's jump key.
    /// </summary>
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    /// <summary>
    /// The player's crouch key.
    /// </summary>
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    /// <summary>
    /// The player's zoom key.
    /// </summary>
    [SerializeField] private KeyCode zoomKey = KeyCode.Mouse1;

    /// <summary>
    /// The player's interact key.
    /// </summary>
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

    /// <summary>
    /// The player's walk speed.
    /// </summary>
    [SerializeField] private float walkSpeed = 3f;

    /// <summary>
    /// The player's crouch speed.
    /// </summary>
    [SerializeField] private float crouchSpeed = 1.5f;

    /// <summary>
    /// The player's x-look speed.
    /// </summary>
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    
    /// <summary>
    /// The player's y-look speed.
    /// </summary>
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    
    /// <summary>
    /// The player's upper look limit.
    /// </summary>
    [SerializeField, Range(1, 180)] private float upperLookLimit = 90;

    /// <summary>
    /// The player's lower look limit.
    /// </summary>
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 90;

    // Jumping Parameters
    /// <summary>
    /// The jump force applied to the player.
    /// </summary>
    [SerializeField] private float jumpForce = 8.0f;

    /// <summary>
    /// The gravity imposed on the player.
    /// </summary>
    [SerializeField] private float gravity = 30f;

    /// <summary>
    /// The jump cooldown of the player.
    /// </summary>
    [SerializeField] private float jumpCooldown = 0.1f;

    /// <summary>
    /// True if the player is ready to jump, false otherwise.
    /// </summary>
    private bool _readyToJump;
    
    /// <summary>
    /// The player's crouching height.
    /// </summary>
    [SerializeField] private float crouchHeight = 0.5f;

    /// <summary>
    /// The player's normal standing height.
    /// </summary>
    [SerializeField] private float standingHeight = 2f;

    /// <summary>
    /// The time it takes for the player to crouch.
    /// </summary>
    [SerializeField] private float timeToCrouch = 0.25f;

    /// <summary>
    /// The center of the crouching player.
    /// </summary>
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);

    /// <summary>
    /// The center of the standing player.
    /// </summary>
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);

    /// <summary>
    /// True if the player is crouching, false otherwise.
    /// </summary>
    private bool _isCrouching;

    /// <summary>
    /// True if the player is undergoing the crouch animation, false otherwise.
    /// </summary>
    private bool _duringCrouchAnimation;
    
    /// <summary>
    /// The time it takes for the player to zoom.
    /// </summary>
    [SerializeField] private float timeToZoom = 0.3f;
    
    /// <summary>
    /// The player's zoomed field of view.
    /// </summary>
    [SerializeField] private float zoomFOV = 30f;

    /// <summary>
    /// The player's default field of view.
    /// </summary>
    private float _defaultFOV;

    /// <summary>
    /// The player's zooming in/out coroutine.
    /// </summary>
    private Coroutine _zoomRoutine;
    
    /// <summary>
    /// The player's interaction ray point.
    /// </summary>
    [SerializeField] private Vector3 interactionRayPoint = new Vector3(0.5f, 0.5f, 0);
    
    /// <summary>
    /// The player's interaction range.
    /// </summary>
    [SerializeField] private float interactionDistance = 2f;

    /// <summary>
    /// The interaction layer mask.
    /// </summary>
    [SerializeField] private LayerMask interactionLayer = 9;
    
    /// <summary>
    /// The player's current interactable object.
    /// </summary>
    private Interactable _currentInteractable;
    
    /// <summary>
    /// The player's camera.
    /// </summary>
    private Camera _playerCamera;

    /// <summary>
    /// The player's controller.
    /// </summary>
    private CharacterController _characterController;

    /// <summary>
    /// The player's movement direction.
    /// </summary>
    private Vector3 _moveDirection;

    /// <summary>
    /// The player's input.
    /// </summary>
    private Vector2 _currentInput;

    /// <summary>
    /// The player's x-rotation
    /// </summary>
    private float _rotationX = 0;
    
    /// <summary>
    /// Gets components and locks the cursor to the middle of the screen.
    /// </summary>
    void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        _defaultFOV = _playerCamera.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _readyToJump = true;
    }
    
    /// <summary>
    /// Controls the player accordingly.
    /// </summary>
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();
            HandleJump();
            HandleCrouch();
            HandleZoom();
            HandleInteractionCheck();
            HandleInteractionInput();
            ApplyFinalMovements();
        }
    }
    
    /// <summary>
    /// Handles player movement input.
    /// </summary>
    private void HandleMovementInput()
    {
        _currentInput = new Vector2((_isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Vertical"),
            (_isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = _moveDirection.y;
        _moveDirection = transform.TransformDirection(Vector3.forward) * _currentInput.x +
                         transform.TransformDirection(Vector3.right) * _currentInput.y;
        _moveDirection.y = moveDirectionY;
    }

    /// <summary>
    /// Handles player mouse input.
    /// </summary>
    private void HandleMouseLook()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -upperLookLimit, lowerLookLimit);
        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    /// <summary>
    /// Handles player jumping.
    /// </summary>
    private void HandleJump()
    {
        if (ShouldJump)
        {
            _readyToJump = false;
            _moveDirection.y = jumpForce;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    /// <summary>
    /// Handles player crouching.
    /// </summary>
    private void HandleCrouch()
    {
        if (ShouldCrouch) StartCoroutine(CrouchStand());
    }
    
    /// <summary>
    /// Handles the player zooming in/out.
    /// </summary>
    private void HandleZoom()
    {
        if (Input.GetKeyDown(zoomKey))
        {
            if (_zoomRoutine != null)
            {
                StopCoroutine(_zoomRoutine);
                _zoomRoutine = null;
            }

            _zoomRoutine = StartCoroutine(ToggleZoom(true));
        }
        
        if (Input.GetKeyUp(zoomKey))
        {
            if (_zoomRoutine != null)
            {
                StopCoroutine(_zoomRoutine);
                _zoomRoutine = null;
            }

            _zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
    }

    /// <summary>
    /// Looks for objects with which the player can interact.
    /// </summary>
    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(_playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit,
                interactionDistance))
        {
            if (hit.collider.gameObject.layer == 9 && 
                (_currentInteractable == null || hit.collider.gameObject.GetInstanceID() != 
                    _currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out _currentInteractable);

                if (_currentInteractable)
                {
                    _currentInteractable.OnFocus();
                }
            }
            else if (_currentInteractable)
            {
                _currentInteractable.OnLoseFocus();
                _currentInteractable = null;
            }
        }
    }

    /// <summary>
    /// Handles player interaction input.
    /// </summary>
    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && _currentInteractable != null && 
            Physics.Raycast(_playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, 
                interactionDistance, interactionLayer))
        {
            _currentInteractable.OnInteract();
        }
    }

    /// <summary>
    /// Applies any final movements to the player.
    /// </summary>
    private void ApplyFinalMovements()
    {
        if (!_characterController.isGrounded) _moveDirection.y -= gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Resets the player's ready to jump status.
    /// </summary>
    private void ResetJump()
    {
        _readyToJump = true;
    }

    /// <summary>
    /// Alters the player's height and center to crouch/stand.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CrouchStand()
    {
        if (_isCrouching && Physics.Raycast(_playerCamera.transform.position, Vector3.up, 1f))
            yield break;


        _duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = _isCrouching ? standingHeight : crouchHeight;
        float currentHeight = _characterController.height;
        Vector3 targetCenter = _isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = _characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            _characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            _characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _characterController.height = targetHeight;
        _characterController.center = targetCenter;

        _isCrouching = !_isCrouching;

        _duringCrouchAnimation = false;
    }

    /// <summary>
    /// Zooms in/out the player's field of view.
    /// </summary>
    /// <param name="isEnter">True if zooming in, false otherwise.</param>
    /// <returns></returns>
    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : _defaultFOV;
        float startingFOV = _playerCamera.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed < timeToZoom)
        {
            _playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        _playerCamera.fieldOfView = targetFOV;
        _zoomRoutine = null;
    }
}