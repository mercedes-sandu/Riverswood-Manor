using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    /// <summary>
    /// The position of the camera.
    /// </summary>
    [SerializeField] private Transform cameraPosition;

    /// <summary>
    /// Moves the camera to the proper position.
    /// </summary>
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}