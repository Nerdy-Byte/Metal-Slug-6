using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D theRB;
    public Animator anim;
    public Animator crouchAnim;
    public float moveSpeed;
    public float jumpForce;
    private bool onGround;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bullet bullet;
    public Transform firePoint;
    public GameObject standing, crouch;
    public float waitToCrouch;
    private float crouch_counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);
        if(theRB.velocity.x < 0){transform.localScale = new Vector3(-1, 1, 1);}
        else if(theRB.velocity.x > 0){transform.localScale = new Vector3(1, 1, 1);}
        onGround = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);
        if (Input.GetButtonDown("Jump") && onGround)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation).bulletDirection = new Vector2(transform.localScale.x, 0);
            anim.SetTrigger("fired");
            crouchAnim.SetTrigger("fired");
        }

        if(!crouch.activeSelf){
            if(Input.GetAxisRaw("Vertical") < -0.9){
                crouch_counter -= Time.deltaTime;
                if(crouch_counter < 0){
                    crouch.SetActive(true);
                    // anim.SetTrigger("crouch");
                    standing.SetActive(false);
                }
            }
            else{
                crouch_counter = waitToCrouch;
            }
        }
        else{
            if(Input.GetAxisRaw("Vertical") > 0.9){
                crouch_counter -= Time.deltaTime;
                if(crouch_counter < 0){
                    crouch.SetActive(false);
                    standing.SetActive(true);
                }
            }
            else{
                crouch_counter = waitToCrouch;
            }
        }

        anim.SetBool("onGround", onGround);
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        crouchAnim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
    }
}
