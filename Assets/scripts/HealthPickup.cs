using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles health pickups that heal the player upon collision.
public class HealthPickup : MonoBehaviour
{
    // Amount of health to restore when the player collects this pickup.
    public int healAmount;

    // This method is called when another collider enters the trigger collider attached to this GameObject.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Check if the object that collided is tagged as "Player".
        if(other.tag == "Player")
        {
            // Heal the player by the specified amount.
            PlayerHealthControler.instance.HealPlayer(healAmount);

            // Destroy the health pickup object after it has been collected.
            Destroy(gameObject);
        }
    }
}
