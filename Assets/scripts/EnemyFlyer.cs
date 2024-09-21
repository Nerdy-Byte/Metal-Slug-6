using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : MonoBehaviour
{
    public float RangeToMove;
    private bool isChasing;
    public float moveSpeed, turnSpeed;
    private Transform player;
    public Animator anim;

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
            if (player.gameObject.activeSelf)
            {
                // Chase and rotate towards the player
                Vector3 direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Only rotate around the Z-axis for 2D rotation
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

                // Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    // Handle collision with the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Damage the player
            PlayerHealthControler.instance.DamagePlayer(1);

            // Trigger the death animation
            anim.SetTrigger("death");

            // Ensure that the flyer is not destroyed immediately, but after a delay
            StartCoroutine(WaitAndDestroy(1f));  // Pass a reasonable delay for the death animation
        }
    }

    // Coroutine to wait for the death animation to finish before destroying the object
    private IEnumerator WaitAndDestroy(float delay)
    {
        // Wait for the delay (time length of death animation)
        yield return new WaitForSeconds(delay);
        
        // Destroy the enemy object after the animation plays out
        Destroy(gameObject);
    }
}
