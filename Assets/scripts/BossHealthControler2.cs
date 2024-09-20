using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthControler2 : MonoBehaviour
{
    public static BossHealthControler2 instance;
    private void Awake(){
        instance = this;
    }
    public int maxHealth;
    public int currentHealth = 30;
    public BossBattle2 theBoss;
    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
        // healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    public void DamageBoss(int damageAmount){
        currentHealth -= damageAmount;
        if(currentHealth <= 0){
            currentHealth = 0;
            theBoss.EndBattle();
        }
        healthSlider.value = currentHealth;
    }
}
