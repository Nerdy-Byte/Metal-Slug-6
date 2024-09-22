using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle2 : MonoBehaviour
{
    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;
    public float activeTime, fadeoutTime, inactiveTime;
    private float activeCountdown, fadeoutCountdown, inactiveCountdown, shootCountdown;
    public Transform[] spawnPoints;
    private Transform targetPoint;
    public Transform theBoss;
    public Animator anim;
    public float moveSpeed;
    public GameObject bullet;
    public Transform firePoint;
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
        FacePlayer();
        if(activeCountdown > 0){
            activeCountdown -= Time.deltaTime;
            if(activeCountdown <= 0){
                fadeoutCountdown = fadeoutTime;
            }

            shootCountdown -= Time.deltaTime;

            if(shootCountdown <= 0){
                anim.SetTrigger("shoot");
                StartCoroutine(ShootWithDelay(.1f)); 
                Instantiate(bullet, firePoint.position, Quaternion.identity);
                shootCountdown = 1.5f;
            }

        }
        else if(fadeoutCountdown > 0){
            fadeoutCountdown -= Time.deltaTime;

            if(fadeoutCountdown <= 0){
                // anim.SetTrigger("fadeout");
                theBoss.gameObject.SetActive(false);
                inactiveCountdown = inactiveTime;

            }
        }
        else if(inactiveCountdown > 0){
            inactiveCountdown -= Time.deltaTime;
            if(inactiveCountdown <= 0){
                theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                firePoint.position = theBoss.position;
                theBoss.gameObject.SetActive(true);
                activeCountdown = activeTime;
            }
        }
        // else{
            
        // }
    }

    public void EndBattle(){
        gameObject.SetActive(false);
        theCam.enabled = true;
        ScoreControler.AddPoints(15);
    }

    void FacePlayer()
    {
        // Get the direction from the boss to the player
        Vector3 direction = PlayerHealthControler.instance.transform.position - theBoss.position;

        // Check if the player is to the left or right of the boss
        if (direction.x < 0)
        {
            // Player is to the right, face right
            theBoss.localScale = new Vector3(Mathf.Abs(theBoss.localScale.x), theBoss.localScale.y, theBoss.localScale.z);
        }
        else if (direction.x > 0)
        {
            // Player is to the left, face left (flip along the X-axis)
            theBoss.localScale = new Vector3(-Mathf.Abs(theBoss.localScale.x), theBoss.localScale.y, theBoss.localScale.z);
        }
    }
    IEnumerator ShootWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay
        Instantiate(bullet, firePoint.position, Quaternion.identity);  // Spawn the bullet after the delay
    }
}
