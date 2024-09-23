using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to control the behavior of the second boss battle.
public class BossBattle2 : MonoBehaviour
{
    // Reference to the CameraController for controlling the camera during the boss battle.
    private CameraController theCam;
    
    // Target position for the camera during the battle.
    public Transform camPosition;
    
    // Speed at which the camera moves to the target position.
    public float camSpeed;
    
    // Timers for different boss states: active, fadeout, and inactive.
    public float activeTime, fadeoutTime, inactiveTime;
    
    // Countdown timers for switching between boss states.
    private float activeCountdown, fadeoutCountdown, inactiveCountdown, shootCountdown;
    
    // Array of spawn points for the boss's teleportation during the battle.
    public Transform[] spawnPoints;
    
    // The target spawn point where the boss will move to.
    private Transform targetPoint;
    
    // Reference to the boss object in the game.
    public Transform theBoss;
    
    // Reference to the Animator component for triggering boss animations.
    public Animator anim;
    
    // Speed at which the boss moves when active.
    public float moveSpeed;
    
    // The bullet prefab that the boss will shoot.
    public GameObject bullet;
    
    // The fire point from where the boss shoots bullets.
    public Transform firePoint;
    
    // Start is called before the first frame update
    // Initializes the camera and sets the countdown for the boss's active state.
    void Start()
    {
        // Disable the CameraController so the camera can be controlled manually during the battle.
        theCam  = FindObjectOfType<CameraController>();
        theCam.enabled = false;

        // Initialize the active state countdown to the specified active time.
        activeCountdown = activeTime;
    }

    // Update is called once per frame
    // Handles camera movement, boss states, and shooting behavior.
    void Update()
    {
        // Move the camera towards the designated position during the battle.
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        
        // Ensure the boss is always facing the player.
        FacePlayer();

        // If the boss is in the active state.
        if (activeCountdown > 0)
        {
            // Decrease the active countdown timer.
            activeCountdown -= Time.deltaTime;

            // When the active countdown ends, switch to the fadeout state.
            if (activeCountdown <= 0)
            {
                fadeoutCountdown = fadeoutTime;
            }

            // Decrease the shooting countdown timer.
            shootCountdown -= Time.deltaTime;

            // When it's time to shoot, trigger the shooting animation and fire a bullet.
            if (shootCountdown <= 0)
            {
                anim.SetTrigger("shoot");
                StartCoroutine(ShootWithDelay(.1f));  // Delay the shooting for a short time.
                Instantiate(bullet, firePoint.position, Quaternion.identity);  // Spawn the bullet.
                shootCountdown = 1.5f;  // Reset the shooting timer.
            }
        }
        // If the boss is in the fadeout state.
        else if (fadeoutCountdown > 0)
        {
            // Decrease the fadeout countdown timer.
            fadeoutCountdown -= Time.deltaTime;

            // When the fadeout ends, make the boss inactive.
            if (fadeoutCountdown <= 0)
            {
                // anim.SetTrigger("fadeout");  // Optional animation trigger for fadeout (commented out).
                theBoss.gameObject.SetActive(false);  // Deactivate the boss.
                inactiveCountdown = inactiveTime;  // Set the inactive countdown.
            }
        }
        // If the boss is in the inactive state.
        else if (inactiveCountdown > 0)
        {
            // Decrease the inactive countdown timer.
            inactiveCountdown -= Time.deltaTime;

            // When the inactive state ends, teleport the boss to a new spawn point and reactivate it.
            if (inactiveCountdown <= 0)
            {
                // Select a random spawn point and move the boss there.
                theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                firePoint.position = theBoss.position;
                theBoss.gameObject.SetActive(true);  // Reactivate the boss.
                activeCountdown = activeTime;  // Reset the active countdown.
            }
        }
    }

    // Method to end the boss battle, reactivate the camera, and add points to the player's score.
    public void EndBattle()
    {
        gameObject.SetActive(false);  // Deactivate the boss battle object.
        theCam.enabled = true;  // Re-enable the camera control.
        ScoreControler.AddPoints(15);  // Add points to the player's score.
    }

    // Method to make the boss face the player at all times.
    void FacePlayer()
    {
        // Calculate the direction from the boss to the player.
        Vector3 direction = PlayerHealthControler.instance.transform.position - theBoss.position;

        // If the player is to the right of the boss, face right.
        if (direction.x < 0)
        {
            theBoss.localScale = new Vector3(Mathf.Abs(theBoss.localScale.x), theBoss.localScale.y, theBoss.localScale.z);
        }
        // If the player is to the left of the boss, face left (flip along the X-axis).
        else if (direction.x > 0)
        {
            theBoss.localScale = new Vector3(-Mathf.Abs(theBoss.localScale.x), theBoss.localScale.y, theBoss.localScale.z);
        }
    }

    // Coroutine to shoot a bullet after a short delay.
    IEnumerator ShootWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay.
        Instantiate(bullet, firePoint.position, Quaternion.identity);  // Spawn the bullet after the delay.
    }
}
