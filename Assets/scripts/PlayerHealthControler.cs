using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthControler : MonoBehaviour
{
    public static PlayerHealthControler instance;

    private void Awake(){
        instance = this;
    }

    // [HideInInspector]
    public int currentHealth;
    public float invincibilityLength;
    private float invincibilityCounter;
    public float flashLength;
    private float flashCounter;
    public int maxHealth=10;
    public SpriteRenderer[] playerSprites;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UI_Controler.instance.UpdateHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0){
            invincibilityCounter -= Time.deltaTime;
        }
        flashCounter -= Time.deltaTime;
        if(flashCounter <= 0){
            foreach(SpriteRenderer sr in playerSprites){
                sr.enabled = !sr.enabled;
            }
            flashCounter = flashLength;
        }
        if(invincibilityCounter <= 0){
            foreach(SpriteRenderer sr in playerSprites){
                sr.enabled = true;
            }
            flashCounter = 0;
        }
    }

    public void DamagePlayer(int damageAmount) 
    {
        if(invincibilityCounter <= 0){
            currentHealth -= damageAmount;
            if(currentHealth <= 0){
                currentHealth = 0;
                gameObject.SetActive(false);
            }
            else{
                invincibilityCounter = invincibilityLength;
            }
            UI_Controler.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }
}
