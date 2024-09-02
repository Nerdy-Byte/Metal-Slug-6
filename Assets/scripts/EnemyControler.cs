using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;
    public float movSpeed;
    public float waitTime;
    private float waitAtPoint;
    private float waitCounter;
    Animator anim;
    public float jumpForce;
    public Rigidbody2D theRB;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
        foreach (Transform item in patrolPoints){
            item.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2){
            if(transform.position.x < patrolPoints[currentPoint].position.x){
                theRB.velocity = new Vector2(movSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(transform.position.x > patrolPoints[currentPoint].position.x){
                theRB.velocity = new Vector2(-movSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(transform.position.y < patrolPoints[currentPoint].position.y - 0.5 && theRB.velocity.y < 0.1){
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
        }
        else{
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            if(waitCounter > 0){
                waitCounter -= Time.deltaTime;
            }
            else{
                waitCounter = waitTime;
                currentPoint++;
                if(currentPoint >= patrolPoints.Length){
                    currentPoint = 0;
                }
            }
        }
    }
}
