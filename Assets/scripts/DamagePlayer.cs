using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles damaging the player when they collide with or enter a trigger zone of this object.
public class DamagePlayer : MonoBehaviour
{
    // Amount of damage to deal to the player.
    public int damageAmount = 10;

    // Called when another collider enters the trigger collider attached to this object.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Check if the object that entered the trigger is the player.
        if (other.tag == "Player")
        {
            // Deal damage to the player.
            DealDamage();
        } 
    }

    // Called when this object collides with another collider.
    private void OnCollisionEnter2D(Collision2D other) 
    {
        // Check if the collided object is the player.
        if (other.gameObject.tag == "Player")
        {
            // Deal damage to the player.
            DealDamage();
        }
    }

    // Method to apply damage to the player using the PlayerHealthController instance.
    void DealDamage()
    {
        // Call the DamagePlayer method on the PlayerHealthController instance to apply damage.
        PlayerHealthControler.instance.DamagePlayer(damageAmount);
    }
}
