using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls the player's movement, jumping, shooting, and crouching behavior.
public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D theRB; // Reference to the player's Rigidbody2D component
    public Animator anim; // Reference to the player's main animator
    public Animator crouchAnim; // Reference to the crouching animator
    public float moveSpeed; // Speed of player movement
    public float jumpForce; // Force applied for jumping
    private bool onGround; // Checks if the player is on the ground
    public LayerMask whatIsGround; // LayerMask to identify ground layers
    public Transform groundCheck; // Position to check for ground contact
    public bullet bullet; // Reference to the bullet prefab
    public Transform firePoint; // Position where the bullet is instantiated
    public GameObject standing, crouch; // GameObjects for standing and crouching states
    public float waitToCrouch; // Time to wait before crouching
    private float crouch_counter; // Counter for crouching delay
    public bool canMove; // Flag to allow or restrict player movement

    // Start is called before the first frame update
    void Start()
    {
        canMove = true; // Initialize movement flag to true
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player can move
        if (canMove)
        {
            // Move the player based on horizontal input
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

            // Flip the player's sprite based on movement direction
            if (theRB.velocity.x < 0) { transform.localScale = new Vector3(-1, 1, 1); }
            else if (theRB.velocity.x > 0) { transform.localScale = new Vector3(1, 1, 1); }

            // Check if the player is on the ground
            onGround = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);

            // Handle jumping
            if (Input.GetButtonDown("Jump") && onGround)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce); // Apply jump force
            }

            // Handle shooting
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation).bulletDirection = new Vector2(transform.localScale.x, 0);
                anim.SetTrigger("fired"); // Trigger firing animation
                crouchAnim.SetTrigger("fired"); // Trigger crouch firing animation
            }

            // Handle crouching
            if (!crouch.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") < -0.9)
                {
                    crouch_counter -= Time.deltaTime; // Count down to crouch
                    if (crouch_counter < 0)
                    {
                        crouch.SetActive(true); // Activate crouch
                        standing.SetActive(false); // Deactivate standing
                    }
                }
                else
                {
                    crouch_counter = waitToCrouch; // Reset counter if not pressing down
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > 0.9)
                {
                    crouch_counter -= Time.deltaTime; // Count down to stand up
                    if (crouch_counter < 0)
                    {
                        crouch.SetActive(false); // Deactivate crouch
                        standing.SetActive(true); // Activate standing
                    }
                }
                else
                {
                    crouch_counter = waitToCrouch; // Reset counter if not pressing up
                }
            }
        }
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y); // Stop horizontal movement if cannot move
        }

        // Set animator parameters
        anim.SetBool("onGround", onGround); // Update ground state in animator
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x)); // Update speed in animator
        crouchAnim.SetFloat("speed", Mathf.Abs(theRB.velocity.x)); // Update speed in crouch animator
    }
}
