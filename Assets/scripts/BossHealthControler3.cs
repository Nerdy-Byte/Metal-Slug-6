using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class BossHealthControler3 : MonoBehaviour
{
    public static BossHealthControler3 instance;

    private void Awake()
    {
        instance = this;
    }

    public int maxHealth;
    public int currentHealth = 30;
    public BossBattle3 theBoss;
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    public void DamageBoss(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            theBoss.EndBattle();
            TriggerCutscene(); // Call the cutscene method
        }
        healthSlider.value = currentHealth;
    }

    private void TriggerCutscene()
    {
        // Load the cutscene scene
        SceneManager.LoadScene("lvl3-complete");
    }
}
