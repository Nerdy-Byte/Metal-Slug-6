using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls the camera movement, ensuring that it follows the player
// while staying within specified bounds.
public class CameraController : MonoBehaviour
{
    // Reference to the player object to follow.
    private PlayerControler player;

    // BoxCollider2D to define the camera's movement boundaries.
    public BoxCollider2D bound;

    // Variables to store the camera's horizontal and vertical limits based on the screen size.
    private float boundX, boundY;

    // Start is called before the first frame update.
    void Start()
    {
        // Find the player in the scene.
        player = FindObjectOfType<PlayerControler>();

        // Set the vertical bound based on the camera's orthographic size.
        boundY = Camera.main.orthographicSize;

        // Set the horizontal bound based on the aspect ratio of the camera.
        boundX = boundY * Camera.main.aspect;
    }

    // Update is called once per frame.
    void Update()
    {
        // If the player exists, adjust the camera's position to follow the player.
        if (player != null)
        {
            // Clamp the camera's X and Y positions so it stays within the bounds.
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, bound.bounds.min.x + boundX, bound.bounds.max.x - boundX), 
                Mathf.Clamp(player.transform.position.y, bound.bounds.min.y + boundY, bound.bounds.max.y - boundY), 
                transform.position.z
            );
        }
    }
}
