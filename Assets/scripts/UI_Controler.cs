using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class manages the UI elements related to health, particularly the health slider.
public class UI_Controler : MonoBehaviour
{
    public static UI_Controler instance; // Singleton instance for easy access

    private void Awake()
    {
        instance = this; // Assign this instance to the singleton
    }

    public Slider healthSlider; // Slider component to display player health

    // Start is called before the first frame update
    void Start()
    {
        // Initialization can be done here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // UI updates can be handled here if needed, currently left empty
    }

    // Method to update the health slider based on current and maximum health
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth; // Set the maximum value of the slider
        healthSlider.value = currentHealth; // Set the current value of the slider
    }
}
