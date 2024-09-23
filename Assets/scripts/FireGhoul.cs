using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGhoul : MonoBehaviour
{
    public float movSpeed;
    public float jumpForce;
    public float RangeToMove;
    private Transform player;
    private bool isChasing = false;
    public Rigidbody2D theRB;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthControler.instance.transform;
    }

    // Update is called once per frame
    void Update()
{
    if (!isChasing)
    {
        // Check if the player is within the chase range
        if (Vector3.Distance(transform.position, player.position) < RangeToMove)
        {
            isChasing = true;
        }
    }
    else
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < RangeToMove)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, player.position, movSpeed * Time.deltaTime);
            // theRB.velocity = new Vector2(movSpeed, theRB.velocity.y);

            // Flip the sprite without rotating it
            if (direction.x > 0) // Player is to the right
            {
                transform.localScale = new Vector3(-7, 7, 1);
            }
            else if (direction.x < 0) // Player is to the left
            {
            // theRB.velocity = new Vector2(movSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(7, 7, 1);
            }
        }
    }

    

}

private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag=="Player"){
        PlayerHealthControler.instance.DamagePlayer(1);
        anim.SetTrigger("death");
        Destroy(gameObject);
    } 
}

}

