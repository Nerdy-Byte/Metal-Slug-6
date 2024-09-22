using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle3 : MonoBehaviour
{
    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;
    public float activeTime, inactiveTime;
    private float activeCountdown, inactiveCountdown;
    public Transform[] firePoints;
    private Transform targetPoint;
    public Transform theBoss;
    public Animator anim;
    private bool hasAttacked = false;

    // public float moveSpeed;
    public GameObject bullet;
    // public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        theCam  = FindObjectOfType<CameraController>();
        theCam.enabled = false;

        activeCountdown = activeTime;
    }

    // Update is called once per frame
    void Update()
    {
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);

        if(activeCountdown > 0){
            activeCountdown -= Time.deltaTime;
            anim.SetTrigger("idle");
            if(activeCountdown <= 0){
                // theBoss.gameObject.SetActive(true);
                inactiveCountdown = inactiveTime;
                hasAttacked = false;
            }
        }
        else if(inactiveCountdown > 0){
            inactiveCountdown -= Time.deltaTime;
            anim.SetTrigger("attack");
            if (!hasAttacked){
                
                for(int i = 0; i < firePoints.Length; i++){
                    Instantiate(bullet, firePoints[i].position, firePoints[i].rotation);
                }
                hasAttacked = true;
            }

            if(inactiveCountdown <= 0){
                activeCountdown = activeTime;
                // theBoss.gameObject.SetActive(false);
            }
        }
        else{
            theBoss.gameObject.SetActive(false);
        }
        
    }

    public void EndBattle(){
        anim.SetTrigger("death");
        gameObject.SetActive(false);
        theCam.enabled = true;
        ScoreControler.AddPoints(15);
    }

    // void FacePlayer()
    // {
    //     // Get the direction from the boss to the player
    //     Vector3 direction = PlayerHealthControler.instance.transform.position - theBoss.position;

    //     // Check if the player is to the left or right of the boss
    //     if (direction.x < 0)
    //     {
    //         // Player is to the right, face right
    //         theBoss.localScale = new Vector3(Mathf.Abs(theBoss.localScale.x), theBoss.localScale.y, theBoss.localScale.z);
    //     }
    //     else if (direction.x > 0)
    //     {
    //         // Player is to the left, face left (flip along the X-axis)
    //         theBoss.localScale = new Vector3(-Mathf.Abs(theBoss.localScale.x), theBoss.localScale.y, theBoss.localScale.z);
    //     }
    // }
    // IEnumerator ShootWithDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);  // Wait for the specified delay
    //     Instantiate(bullet, firePoint.position, Quaternion.identity);  // Spawn the bullet after the delay
    // }
}
