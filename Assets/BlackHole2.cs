using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole2 : MonoBehaviour
{

    [SerializeField]
    float attractForce, attractRange = 10;
    Transform objectToMove;
    Transform player;

    private void Start()
    {
        player = GameManager.instance.player.transform;
    }


    private void FixedUpdate()
    {
        if(Vector3.Distance(player.position, transform.position) < attractRange) //check to see if player is in range
        {
            Debug.Log("sees player");
            Debug.Log(transform.position);
            player.Translate(((transform.position * Time.deltaTime)));
            //Vector3.MoveTowards(player.position, transform.position, attractForce * Time.deltaTime);
        }
    }


    
}
