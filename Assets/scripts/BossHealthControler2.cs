using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import for handling scene transitions

// This class is responsible for managing the health of the boss in BossBattle2.
public class BossHealthControler2 : MonoBehaviour
{
    // Singleton instance for global access to this script.
    public static BossHealthControler2 instance;

    // Called before Start(), used to initialize the singleton instance.
    private void Awake()
    {
        instance = this;
    }

    // Maximum health of the boss, set in the inspector or other scripts.
    public int maxHealth;

    // Current health of the boss, initialized to 30 by default.
    public int currentHealth = 30;

    // Reference to the BossBattle2 script for controlling the boss's behavior when defeated.
    public BossBattle2 theBoss;

    // Reference to the UI slider that displays the boss's health.
    public Slider healthSlider;

    // Start is called before the first frame update.
    // Initializes the health slider with the boss's current health.
    void Start()
    {
        // Set the maximum value of the slider to the boss's initial health.
        healthSlider.maxValue = currentHealth;
        
        // Set the slider's value to the current health at the start of the battle.
        healthSlider.value = currentHealth;
    }

    // Method to apply damage to the boss and check if the boss's health reaches zero.
    public void DamageBoss(int damageAmount)
    {
        // Subtract the damage from the boss's current health.
        currentHealth -= damageAmount;
        
        // If the boss's health drops to zero or below.
        if (currentHealth <= 0)
        {
            // Ensure the health doesn't go negative.
            currentHealth = 0;
            
            // End the boss battle using the EndBattle() method from BossBattle2.
            theBoss.EndBattle();
            
            // Trigger the cutscene after defeating the boss.
            TriggerCutscene();
        }
        
        // Update the health slider to reflect the boss's current health.
        healthSlider.value = currentHealth;
    }

    // Method to trigger the cutscene when the boss is defeated.
    private void TriggerCutscene()
    {
        // Load the cutscene scene (in this case, "lvl2-complete").
        SceneManager.LoadScene("lvl2-complete");
    }
}
