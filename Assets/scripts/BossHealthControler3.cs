using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import for handling scene transitions

// This class manages the health of the boss in BossBattle3.
public class BossHealthControler3 : MonoBehaviour
{
    // Singleton instance to allow global access to this script.
    public static BossHealthControler3 instance;

    // Called before Start(), sets up the singleton instance.
    private void Awake()
    {
        instance = this;
    }

    // Maximum health of the boss, to be set in the inspector or programmatically.
    public int maxHealth;

    // Current health of the boss, initialized to 30.
    public int currentHealth = 30;

    // Reference to the BossBattle3 script to manage boss behavior after defeat.
    public BossBattle3 theBoss;

    // Reference to the UI slider that displays the boss's health.
    public Slider healthSlider;

    // Start is called before the first frame update.
    // Initializes the health slider with the boss's current health.
    void Start()
    {
        // Set the maximum value of the health slider to the boss's initial health.
        healthSlider.maxValue = currentHealth;
        
        // Set the slider value to the current health at the beginning of the battle.
        healthSlider.value = currentHealth;
    }

    // Method to apply damage to the boss and check if the boss's health reaches zero.
    public void DamageBoss(int damageAmount)
    {
        // Subtract the damage from the boss's current health.
        currentHealth -= damageAmount;
        
        // If the boss's health reaches zero or below.
        if (currentHealth <= 0)
        {
            // Ensure the health doesn't drop below zero.
            currentHealth = 0;
            
            // Call the EndBattle() method from BossBattle3 to end the battle.
            theBoss.EndBattle();
            
            // Trigger the cutscene after the boss is defeated.
            TriggerCutscene();
        }
        
        // Update the health slider to show the current health of the boss.
        healthSlider.value = currentHealth;
    }

    // Method to trigger the cutscene after the boss is defeated.
    private void TriggerCutscene()
    {
        // Load the cutscene scene (in this case, "lvl3-complete").
        SceneManager.LoadScene("lvl3-complete");
    }
}
