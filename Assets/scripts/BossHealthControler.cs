using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import to manage scene transitions

// Class responsible for controlling the boss's health and managing health-based interactions.
public class BossHealthControler : MonoBehaviour
{
    // Singleton instance of the BossHealthControler to allow global access.
    public static BossHealthControler instance;

    // Method called before the Start() method, ensuring the singleton instance is set up.
    private void Awake()
    {
        instance = this;
    }

    // Reference to the UI Slider that displays the boss's health.
    public Slider bossHealthSlider;
    
    // Variable for the boss's current health, initialized to 30.
    public int currHealth = 30;
    
    // Reference to the BossBattle1 script, used to interact with the boss when health reaches zero.
    public BossBattle1 theBoss;

    // Start is called before the first frame update
    // Sets up the boss health slider with the maximum health.
    void Start()
    {
        // Set the maximum value of the slider to match the boss's initial health.
        bossHealthSlider.maxValue = currHealth;
        
        // Initialize the slider's value to the current health.
        bossHealthSlider.value = currHealth;
    }

    // Method to apply damage to the boss and handle events when health reaches zero.
    public void DamageBoss(int damageAmount)
    {
        // Subtract the damage amount from the boss's current health.
        currHealth -= damageAmount;
        
        // If the boss's health drops to zero or below.
        if (currHealth <= 0)
        {
            // Ensure the health value doesn't go negative.
            currHealth = 0;
            
            // End the boss battle by calling the EndBattle() method from BossBattle1.
            theBoss.EndBattle();
            
            // Trigger the cutscene after the boss is defeated.
            TriggerCutscene();
        }
        
        // Update the health slider to reflect the boss's current health.
        bossHealthSlider.value = currHealth;
    }

    // Method to load a cutscene when the boss is defeated.
    private void TriggerCutscene()
    {
        // Load the scene containing the cutscene (in this case, "lvl1-complete").
        SceneManager.LoadScene("lvl1-complete");
    }
}
