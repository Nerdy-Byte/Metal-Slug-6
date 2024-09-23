using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls the behavior of the bullet in the game.
public class bullet : MonoBehaviour
{
    // Speed at which the bullet travels.
    public float bulletSpeed;

    // Rigidbody2D component of the bullet, used to control physics behavior.
    public Rigidbody2D theRb;

    // Amount of damage the bullet deals.
    public float bulletDamage;

    // The direction in which the bullet moves.
    public Vector2 bulletDirection;

    // The amount of damage the bullet inflicts, initialized to 1.
    public int damageAmount = 1;

    // Reference to the visual effect that plays when the bullet hits something.
    public GameObject bulletEffect;

    // Start is called before the first frame update.
    void Start()
    {
        // Initialization logic can go here, if needed.
    }

    // Update is called once per frame.
    // Moves the bullet in the specified direction at the given speed.
    void Update()
    {
        // Set the velocity of the bullet's Rigidbody2D to move it.
        theRb.velocity = bulletDirection * bulletSpeed;
    }

    // Method called when the bullet collides with another object.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If the bullet hits an enemy, deal damage to the enemy.
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthControler>().DamageEnemy(damageAmount);
        }

        // If the bullet hits the first boss, deal damage to the boss.
        if(other.tag == "Boss")
        {
            BossHealthControler.instance.DamageBoss(damageAmount);
        }

        // If the bullet hits the second boss, deal damage to the second boss.
        if(other.tag == "Boss2")
        {
            BossHealthControler2.instance.DamageBoss(damageAmount);
        }

        // If the bullet hits the third boss, deal damage to the third boss.
        if(other.tag == "Boss3")
        {
            BossHealthControler3.instance.DamageBoss(damageAmount);
        }

        // If there is a bullet effect, instantiate it at the current bullet position.
        if(bulletEffect != null)
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
        
        // Destroy the bullet after it hits something.
        Destroy(gameObject);
    }

    // This method is called when the bullet goes off-screen.
    private void OnBecameInvisible() 
    {
        // Destroy the bullet if it leaves the screen to avoid unnecessary objects in the scene.
        Destroy(gameObject);
    }
}
