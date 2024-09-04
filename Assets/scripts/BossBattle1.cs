using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle1 : MonoBehaviour
{
    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;
    public Animator anim;
    public float timeBetweenshot1, timeBetweenshot2;
    private float shotCounter;
    public GameObject bullet;
    public Transform firePoint;
    void Start()
    {
        theCam = FindObjectOfType<CameraController>();
        theCam.enabled = false; 
        shotCounter = Random.Range(timeBetweenshot1, timeBetweenshot2);
    }

    // Update is called once per frame
    void Update()
    {
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0){
            anim.SetTrigger("fire1");
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            shotCounter = Random.Range(timeBetweenshot1, timeBetweenshot2);
            
        }
    }

    public void EndBattle(){
        gameObject.SetActive(false);
        theCam.enabled = true;
    }
}

