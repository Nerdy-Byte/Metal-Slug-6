using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D theRb;
    public float bulletDamage;
    public Vector2 bulletDirection;
    public int damageAmount=1; 
    public GameObject bulletEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRb.velocity = bulletDirection * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Enemy"){
            other.GetComponent<EnemyHealthControler>().DamageEnemy(damageAmount);
        }

        if(other.tag=="Boss"){
            BossHealthControler.instance.DamageBoss(damageAmount);
        }
        if(bulletEffect!=null)
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
