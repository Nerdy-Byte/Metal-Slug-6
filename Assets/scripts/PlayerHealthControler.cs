using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthControler : MonoBehaviour
{
    public static PlayerHealthControler instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentHealth;
    public float invincibilityLength;
    private float invincibilityCounter;
    public float flashLength;
    private float flashCounter;
    public int maxHealth = 10;
    public SpriteRenderer[] playerSprites;

    // Animator reference
    private Animator animator;

    // Audio components
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip healthPickupSound;

    void Start()
    {
        currentHealth = maxHealth;
        UI_Controler.instance.UpdateHealth(currentHealth, maxHealth);
        animator = GetComponentInChildren<Animator>(); // Assuming the Animator is on the child object
    }

    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
        flashCounter -= Time.deltaTime;
        if (flashCounter <= 0)
        {
            foreach (SpriteRenderer sr in playerSprites)
            {
                sr.enabled = !sr.enabled;
            }
            flashCounter = flashLength;
        }
        if (invincibilityCounter <= 0)
        {
            foreach (SpriteRenderer sr in playerSprites)
            {
                sr.enabled = true;
            }
            flashCounter = 0;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damageAmount;

            // Play damage sound
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound);
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                StartCoroutine(HandleDeath()); // Start the death handling coroutine
            }
            else
            {
                invincibilityCounter = invincibilityLength;
            }
            UI_Controler.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    private IEnumerator HandleDeath()
    {
        // Trigger death animation
        animator.SetTrigger("Die"); // Trigger death animation

        // Wait for the animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 3f);

        // Load the game over scene
        SceneManager.LoadScene("Gameover");
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Play health pickup sound
        if (audioSource != null && healthPickupSound != null)
        {
            audioSource.PlayOneShot(healthPickupSound);
        }

        UI_Controler.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
