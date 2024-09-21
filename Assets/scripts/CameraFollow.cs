using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;  // Offset for the camera position
    public float smoothSpeed = 0.125f;  // Speed of camera movement

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // If your game is 2D, lock the Z position so the camera stays fixed on that axis
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
