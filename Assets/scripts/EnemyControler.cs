using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls the behavior of an enemy, allowing it to patrol between specified points.
public class EnemyControler : MonoBehaviour
{
    // Array of patrol points for the enemy to move between.
    public Transform[] patrolPoints;

    // Index of the current patrol point the enemy is moving towards.
    private int currentPoint;

    // Speed of the enemy's movement.
    public float movSpeed;

    // Time the enemy will wait at each patrol point before moving to the next.
    public float waitTime;

    // Counter for how long the enemy should wait at the current patrol point.
    private float waitAtPoint;

    // Counter for the wait time.
    private float waitCounter;

    // Animator component for controlling animations (if any).
    Animator anim;

    // Force applied when the enemy jumps.
    public float jumpForce;

    // Rigidbody2D component for physics interactions.
    public Rigidbody2D theRB;

    // Start is called before the first frame update.
    void Start()
    {
        // Initialize the wait counter to the wait time.
        waitCounter = waitAtPoint;

        // Ensure that the patrol points are not parented to this object.
        foreach (Transform item in patrolPoints)
        {
            item.SetParent(null);
        }
    }

    // Update is called once per frame.
    void Update()
    {
        // Check if the enemy is far from the current patrol point.
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2)
        {
            // Move towards the patrol point.
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                // Move right and flip the enemy's scale to face right.
                theRB.velocity = new Vector2(movSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x > patrolPoints[currentPoint].position.x)
            {
                // Move left and flip the enemy's scale to face left.
                theRB.velocity = new Vector2(-movSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }

            // Jump if the enemy is below the patrol point and is falling.
            if (transform.position.y < patrolPoints[currentPoint].position.y - 0.5 && theRB.velocity.y < 0.1)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
        }
        else
        {
            // Stop horizontal movement and check if the enemy should wait at the patrol point.
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            if (waitCounter > 0)
            {
                // Decrease the wait counter.
                waitCounter -= Time.deltaTime;
            }
            else
            {
                // Reset the wait counter and move to the next patrol point.
                waitCounter = waitTime;
                currentPoint++;
                if (currentPoint >= patrolPoints.Length)
                {
                    // Loop back to the first patrol point.
                    currentPoint = 0;
                }
            }
        }
    }
}
