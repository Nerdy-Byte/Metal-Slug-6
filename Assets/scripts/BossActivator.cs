using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to activate the boss when the player enters a specific trigger area.
public class BossActivator : MonoBehaviour
{
    // Reference to the boss GameObject that should be activated.
    public GameObject bossToActivate;
    
    // This method is called when another object enters the trigger collider attached to this object.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Check if the object that entered the trigger is tagged as "Player".
        if(other.tag == "Player")
        {
            // Activate the boss by setting its GameObject to active.
            bossToActivate.SetActive(true);
            
            // Deactivate this object (the activator) so it doesn't trigger again.
            gameObject.SetActive(false);
        } 
    }
}
