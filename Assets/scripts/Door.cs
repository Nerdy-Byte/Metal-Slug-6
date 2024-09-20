using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Animator anim;
    public float distanceToOpen;
    private PlayerControler player;
    private bool playerExiting;
    public Transform exitPoint;
    public float moveSpeed;
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthControler.instance.GetComponent<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < distanceToOpen){
            anim.SetBool("doorOpen", true);
        }
        else{
            anim.SetBool("doorOpen", false);
        }

        if(playerExiting){
            if(player.transform.position != exitPoint.position){
                player.transform.position = Vector3.MoveTowards(player.transform.position, exitPoint.position, moveSpeed * Time.deltaTime);
            }
            else{
                playerExiting = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            if(!playerExiting){
                player.canMove = false;
                StartCoroutine(PlayerExit());
            }
        }
    }
    IEnumerator PlayerExit(){
        playerExiting = true;
        player.anim.enabled = false;
        yield return new WaitForSeconds(1.5f);
        playerExiting = false;
        player.canMove = true;
        player.anim.enabled = true;

        SceneManager.LoadScene(levelToLoad);
    }
}
