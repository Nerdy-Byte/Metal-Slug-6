using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D theRB;
    public Animator anim;
    public float moveSpeed;
    public float jumpForce;
    private bool onGround;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bullet bullet;
    public Transform firePoint;
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
        if (Input.GetButtonDown("Fire1") && onGround)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation).bulletDirection = new Vector2(transform.localScale.x, 0);
            anim.SetTrigger("fired");
        }
        anim.SetBool("onGround", onGround);
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
    }
}
