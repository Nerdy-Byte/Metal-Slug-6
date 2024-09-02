using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmount = 10;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            DealDamage();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            DealDamage();
        }
    }

    void DealDamage(){
        PlayerHealthControler.instance.DamagePlayer(damageAmount);
    }
}
