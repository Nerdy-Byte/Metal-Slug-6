using UnityEngine;

// This class handles the camera's following behavior for a player object.
public class CameraFollow : MonoBehaviour
{
    // Reference to the player's transform for positioning the camera.
    public Transform player;  

    // Offset for the camera's position relative to the player.
    public Vector3 offset;  

    // Speed at which the camera will smooth its movement.
    public float smoothSpeed = 0.125f;  

    // LateUpdate is called after all Update methods, ensuring smooth camera movement.
    void LateUpdate()
    {
        // Calculate the desired position of the camera based on the player's position and the offset.
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between the current position and the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position to the smoothed position.
        transform.position = smoothedPosition;

        // If your game is 2D, lock the Z position so the camera stays fixed on that axis.
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
