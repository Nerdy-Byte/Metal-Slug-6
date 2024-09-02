using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControler : MonoBehaviour
{
    public int totalHealth;
    public Animator anim;
    public GameObject deathEffect;
    // Start is called before the first frame update
    public void DamageEnemy(int damageAmount) 
    {
        totalHealth -= damageAmount;
        if(totalHealth <= 0){
            anim.SetTrigger("death");
            Destroy(gameObject, .6f);
        }
    }
}
