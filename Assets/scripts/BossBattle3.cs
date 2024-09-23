using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to control the behavior of the third boss battle.
public class BossBattle3 : MonoBehaviour
{
    // Reference to the CameraController for managing the camera during the boss battle.
    private CameraController theCam;
    
    // Target position for the camera to move to during the battle.
    public Transform camPosition;
    
    // Speed at which the camera moves to the target position.
    public float camSpeed;
    
    // Timers for the boss's active and inactive states.
    public float activeTime, inactiveTime;
    
    // Countdown timers for the active and inactive states.
    private float activeCountdown, inactiveCountdown;
    
    // Array of fire points from which the boss shoots bullets.
    public Transform[] firePoints;
    
    // Reference to the boss's transform component.
    private Transform targetPoint;
    public Transform theBoss;
    
    // Reference to the Animator component to trigger animations.
    public Animator anim;
    
    // Flag to ensure the boss attacks only once per inactive phase.
    private bool hasAttacked = false;

    // Reference to the bullet prefab the boss will shoot.
    public GameObject bullet;
    
    // Start is called before the first frame update
    // Initializes camera control and sets the active state countdown timer.
    void Start()
    {
        // Disable the CameraController so it can be manually controlled during the battle.
        theCam  = FindObjectOfType<CameraController>();
        theCam.enabled = false;

        // Set the active countdown to the initial active time.
        activeCountdown = activeTime;
    }

    // Update is called once per frame
    // Handles the camera movement and controls the boss's active and inactive phases.
    void Update()
    {
        // Move the camera towards the designated position during the battle.
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);

        // If the boss is in the active state.
        if (activeCountdown > 0)
        {
            // Decrease the active countdown timer.
            activeCountdown -= Time.deltaTime;

            // Trigger the idle animation while the boss is active.
            anim.SetTrigger("idle");

            // When the active phase ends, switch to the inactive phase and reset the attack flag.
            if (activeCountdown <= 0)
            {
                inactiveCountdown = inactiveTime;
                hasAttacked = false;
            }
        }
        // If the boss is in the inactive state.
        else if (inactiveCountdown > 0)
        {
            // Decrease the inactive countdown timer.
            inactiveCountdown -= Time.deltaTime;

            // Trigger the attack animation during the inactive phase.
            anim.SetTrigger("attack");

            // Ensure the boss only attacks once per inactive phase.
            if (!hasAttacked)
            {
                // Loop through the fire points and shoot a bullet from each one.
                for (int i = 0; i < firePoints.Length; i++)
                {
                    Instantiate(bullet, firePoints[i].position, firePoints[i].rotation);
                }
                hasAttacked = true;  // Mark the attack as complete.
            }

            // When the inactive phase ends, reset the active countdown timer.
            if (inactiveCountdown <= 0)
            {
                activeCountdown = activeTime;
            }
        }
        // If both active and inactive phases are over, deactivate the boss.
        else
        {
            theBoss.gameObject.SetActive(false);
        }
    }

    // Method to end the boss battle, trigger the death animation, and reward the player with points.
    public void EndBattle()
    {
        anim.SetTrigger("death");  // Trigger the death animation.
        gameObject.SetActive(false);  // Deactivate the boss battle object.
        theCam.enabled = true;  // Re-enable the CameraController.
        ScoreControler.AddPoints(15);  // Add points to the player's score.
    }
}
