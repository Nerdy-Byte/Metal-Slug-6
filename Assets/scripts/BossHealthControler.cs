using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this line

public class BossHealthControler : MonoBehaviour
{
    public static BossHealthControler instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider bossHealthSlider;
    public int currHealth = 30;
    public BossBattle1 theBoss;

    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currHealth;
        bossHealthSlider.value = currHealth;
    }

    public void DamageBoss(int damageAmount)
    {
        currHealth -= damageAmount;
        if (currHealth <= 0)
        {
            currHealth = 0;
            theBoss.EndBattle();
            TriggerCutscene(); // Call the cutscene method
        }
        bossHealthSlider.value = currHealth;
    }

    private void TriggerCutscene()
    {
        // Load the cutscene scene
        SceneManager.LoadScene("lvl1-complete");
    }
}
