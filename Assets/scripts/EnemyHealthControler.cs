using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls the health and damage mechanics of an enemy.
public class EnemyHealthControler : MonoBehaviour
{
    // Total health of the enemy.
    public int totalHealth;

    // Animator for controlling enemy animations.
    public Animator anim;

    // Effect to instantiate upon the enemy's death.
    public GameObject deathEffect;

    // Method to damage the enemy.
    public void DamageEnemy(int damageAmount) 
    {
        // Reduce the enemy's health by the damage amount.
        totalHealth -= damageAmount;

        // Check if the enemy's health has dropped to zero or below.
        if(totalHealth <= 0)
        {
            // Trigger the death animation.
            anim.SetTrigger("death");

            // Destroy the enemy game object after a short delay.
            Destroy(gameObject, .6f); 

            // Award points to the score controller for defeating the enemy.
            ScoreControler.AddPoints(5);
        }
    }
}
