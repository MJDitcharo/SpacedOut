using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole2 : MonoBehaviour
{

    [SerializeField]
    float attractForce, attractRange, moveX, moveZ;
    Vector3 moveDirection;
    Transform objectToMove;
    Transform player;
    private void Start()
    {
        player = GameManager.instance.player.transform;
    }


    private void FixedUpdate()
    {
        if (Vector3.Distance(player.position, transform.position) < attractRange) //check to see if player is in range
        {
            player.parent = transform;



            Vector3 localPos = player.InverseTransformPoint(transform.position);
            Debug.Log(localPos);

            //determine which way to attract to 
            //if (player.position.z > 0)
            //    moveZ = attractForce;
            //else
            //    moveZ = -attractForce;

            //if (player.position.x > 0)
            //    moveX = attractForce;
            //else
            //    moveX = -attractForce;

            //moveDirection = new Vector3(moveX, 0, moveZ);

            //player.Translate(((moveDirection * Time.deltaTime)));
            //Vector3.MoveTowards(player.position, transform.position, attractForce * Time.deltaTime);
        }
    }



}
