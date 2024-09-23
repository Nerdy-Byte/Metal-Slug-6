using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to control the behavior of the boss's bullet in the game.
public class Boss1Bullet : MonoBehaviour
{
    // The speed at which the bullet moves.
    public float moveSpeed; 
    
    // The amount of damage the bullet deals to the player upon collision.
    public int damageAmount;
    
    // Reference to the Rigidbody2D component for handling the bullet's physics.
    public Rigidbody2D theRB;
    
    // Reference to an optional impact effect prefab (commented out for now).
    // public GameObject impactEffect;

    // Start is called before the first frame update
    // Sets the rotation of the bullet to point towards the player when it's fired.
    void Start()
    {
        // Calculate the direction from the bullet to the player.
        Vector3 direction = transform.position - PlayerHealthControler.instance.transform.position;
        
        // Calculate the angle in degrees to rotate the bullet towards the player.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Rotate the bullet to face the player using the calculated angle.
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    // Moves the bullet in the direction it is facing (right direction).
    void Update()
    {
        // Move the bullet to the left (-transform.right) based on its moveSpeed.
        theRB.velocity = -transform.right * moveSpeed;
    }

    // Handles collision with other objects.
    private void OnCollisionEnter2D(Collision2D other) 
    {
        // If the bullet collides with the player, deal damage.
        if(other.gameObject.tag == "Player")
        {
            // Apply damage to the player using the damageAmount value.
            PlayerHealthControler.instance.DamagePlayer(damageAmount);
        }
        
        // Optionally instantiate an impact effect (commented out).
        // Instantiate(impactEffect, transform.position, transform.rotation);
        
        // Destroy the bullet after collision to prevent further interactions.
        Destroy(gameObject);
    }
}
