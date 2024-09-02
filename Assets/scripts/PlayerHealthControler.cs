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
    public int maxHealth=10;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damageAmount) 
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0){
            currentHealth = 0;
            gameObject.SetActive(false);
        }
    }
}
