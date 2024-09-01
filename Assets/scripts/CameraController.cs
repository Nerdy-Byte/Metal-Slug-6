using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerControler player;
    public BoxCollider2D bound;
    private float boundX, boundY;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControler>(); 
        boundY = Camera.main.orthographicSize;
        boundX = boundY*Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null){
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, bound.bounds.min.x + boundX, bound.bounds.max.x - boundX), 
            Mathf.Clamp(player.transform.position.y, bound.bounds.min.y + boundY, bound.bounds.max.y - boundY), transform.position.z);
            }
    }
}
