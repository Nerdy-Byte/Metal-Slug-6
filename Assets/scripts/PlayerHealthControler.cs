using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class manages the player's health, damage, healing, and invincibility state.
public class PlayerHealthControler : MonoBehaviour
{
    public static PlayerHealthControler instance; // Singleton instance for easy access

    private void Awake()
    {
        instance = this; // Assign this instance to the singleton
    }

    // Current health of the player
    // [HideInInspector]
    public int currentHealth;
    public float invincibilityLength; // Duration of invincibility after taking damage
    private float invincibilityCounter; // Counter for invincibility time
    public float flashLength; // Duration for sprite flashing effect
    private float flashCounter; // Counter for flashing effect
    public int maxHealth = 10; // Maximum health of the player
    public SpriteRenderer[] playerSprites; // Array of player sprites for flashing effect

    // Audio components
    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip damageSound; // Sound played on taking damage
    public AudioClip healthPickupSound; // Sound played on healing

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
        UI_Controler.instance.UpdateHealth(currentHealth, maxHealth); // Update UI with initial health
    }

    // Update is called once per frame
    void Update()
    {
        // Manage invincibility timer
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime; // Decrease counter
        }

        // Manage sprite flashing effect
        flashCounter -= Time.deltaTime;
        if (flashCounter <= 0)
        {
            foreach (SpriteRenderer sr in playerSprites)
            {
                sr.enabled = !sr.enabled; // Toggle visibility
            }
            flashCounter = flashLength; // Reset flash counter
        }

        // Reset flashing if invincibility is over
        if (invincibilityCounter <= 0)
        {
            foreach (SpriteRenderer sr in playerSprites)
            {
                sr.enabled = true; // Ensure sprites are visible
            }
            flashCounter = 0; // Reset flash counter
        }
    }

    // Method to handle player damage
    public void DamagePlayer(int damageAmount)
    {
        if (invincibilityCounter <= 0) // Check if player is not invincible
        {
            currentHealth -= damageAmount; // Reduce health by damage amount

            // Play damage sound
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound); // Play sound effect
            }

            if (currentHealth <= 0) // Check if health is zero or less
            {
                currentHealth = 0; // Set health to zero
                gameObject.SetActive(false); // Deactivate player object
                SceneManager.LoadScene("Gameover"); // Load the "Gameover" scene
            }
            else
            {
                invincibilityCounter = invincibilityLength; // Apply invincibility
            }

            // Update health in the UI
            UI_Controler.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    // Method to heal the player
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount; // Increase current health
        if (currentHealth > maxHealth) // Cap health at maximum
        {
            currentHealth = maxHealth;
        }

        // Play health pickup sound
        if (audioSource != null && healthPickupSound != null)
        {
            audioSource.PlayOneShot(healthPickupSound); // Play sound effect
        }

        // Update health in the UI
        UI_Controler.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
