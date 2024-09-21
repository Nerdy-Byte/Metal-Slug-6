using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    // public PlayerHealthControler playerHealthControler;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            PlayerHealthControler.instance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}
