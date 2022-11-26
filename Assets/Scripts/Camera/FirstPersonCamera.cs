using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    /// <summary>
    /// The orientation of the camera.
    /// </summary>
    [SerializeField] private Transform orientation;
    
    /// <summary>
    /// The x-sensitivity of the camera.
    /// </summary>
    [SerializeField] private float sensitivityX;
    
    /// <summary>
    /// The y-sensitivity of the camera.
    /// </summary>
    [SerializeField] private float sensitivityY;

    /// <summary>
    /// The x-rotation of the camera.
    /// </summary>
    private float _rotationX;
    
    /// <summary>
    /// The y-rotation of the camera.
    /// </summary>
    private float _rotationY;
    
    /// <summary>
    /// Lock the cursor to the center of the screen.
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Moves the camera according to mouse movement.
    /// </summary>
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensitivityY;
        
        _rotationY += mouseX;
        _rotationX -= mouseY;
        
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
        orientation.rotation = Quaternion.Euler(0f, _rotationY, 0f);
    }
}