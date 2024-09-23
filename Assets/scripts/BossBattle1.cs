using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to control the behavior of the first boss battle.
public class BossBattle1 : MonoBehaviour
{
    // Reference to the CameraController to control camera movement during the battle.
    private CameraController theCam;
    
    // Target position for the camera during the boss battle.
    public Transform camPosition;
    
    // Speed at which the camera moves to the target position.
    public float camSpeed;
    
    // Reference to the Animator component to trigger animations.
    public Animator anim;
    
    // Time intervals between consecutive shots from the boss.
    public float timeBetweenshot1, timeBetweenshot2;
    
    // Timer to control the next shot.
    private float shotCounter;
    
    // The bullet prefab that the boss will fire.
    public GameObject bullet;
    
    // The fire point from where the boss will shoot the bullet.
    public Transform firePoint;
    
    // Start is called before the first frame update
    // Initializes the camera and sets up the initial shot timer.
    void Start()
    {
        // Find and disable the CameraController to take control of the camera.
        theCam = FindObjectOfType<CameraController>();
        theCam.enabled = false;
        
        // Set a random initial value for the shot timer between the two defined shot intervals.
        shotCounter = Random.Range(timeBetweenshot1, timeBetweenshot2);
    }

    // Update is called once per frame
    // Handles camera movement and shooting behavior during the boss battle.
    void Update()
    {
        // Move the camera towards the target position at the specified speed.
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        
        // Decrease the shot timer by the time elapsed since the last frame.
        shotCounter -= Time.deltaTime;

        // When the shot timer reaches zero or below, trigger a shot.
        if(shotCounter <= 0)
        {
            // Trigger the boss's firing animation.
            anim.SetTrigger("fire1");
            
            // Instantiate (spawn) a bullet at the fire point position.
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            
            // Reset the shot timer to a new random value between the two shot intervals.
            shotCounter = Random.Range(timeBetweenshot1, timeBetweenshot2);
        }
    }

    // Method to end the boss battle, resetting the camera and adding points to the player's score.
    public void EndBattle()
    {
        // Deactivate the boss object.
        gameObject.SetActive(false);
        
        // Re-enable the CameraController to restore normal camera control.
        theCam.enabled = true;
        
        // Add points to the player's score upon defeating the boss.
        ScoreControler.AddPoints(15);
    }
}
