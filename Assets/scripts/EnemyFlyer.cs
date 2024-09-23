using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls the behavior of a flying enemy that chases the player within a certain range.
public class EnemyFlyer : MonoBehaviour
{
    // Distance within which the enemy will start chasing the player.
    public float RangeToMove;

    // Flag indicating whether the enemy is currently chasing the player.
    private bool isChasing;

    // Speed at which the enemy moves and turns.
    public float moveSpeed, turnSpeed;

    // Reference to the player's transform for tracking.
    private Transform player;

    // Animator for controlling enemy animations.
    public Animator anim;

    // Start is called before the first frame update.
    void Start()
    {
        // Get the player's transform from the PlayerHealthControler instance.
        player = PlayerHealthControler.instance.transform;
    }

    // Update is called once per frame.
    void Update()
    {
        if (!isChasing)
        {
            // Check if the player is within the chase range.
            if (Vector3.Distance(transform.position, player.position) < RangeToMove)
            {
                isChasing = true; // Start chasing the player.
            }
        }
        else
        {
            if (player.gameObject.activeSelf)
            {
                // Calculate the direction to the player and the angle for rotation.
                Vector3 direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Create a target rotation based on the player's position.
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                // Smoothly rotate towards the player.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

                // Move towards the player.
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    // Handle collision with the player.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Damage the player upon collision.
            PlayerHealthControler.instance.DamagePlayer(1);

            // Trigger the death animation for the enemy.
            anim.SetTrigger("death");

            // Start a coroutine to wait before destroying the enemy.
            StartCoroutine(WaitAndDestroy(1f));  // Pass a reasonable delay for the death animation.
        }
    }

    // Coroutine to wait for the death animation to finish before destroying the object.
    private IEnumerator WaitAndDestroy(float delay)
    {
        // Wait for the specified delay (duration of the death animation).
        yield return new WaitForSeconds(delay);
        
        // Destroy the enemy object after the animation completes.
        Destroy(gameObject);
        ScoreControler.AddPoints(5); // Add points to the score for defeating the enemy.
    }
}
