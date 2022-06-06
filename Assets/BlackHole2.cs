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
            Vector3 localPos = player.localPosition;
            Debug.Log(localPos);

            //determine which way to attract to 
            if (player.localPosition.z > 0)
                moveZ = 1;
            else
                moveZ = -1;

            if (player.localPosition.x > 0)
                moveX = 1;
            else
                moveX = -1;

            moveDirection = new Vector3(moveX, 0, moveZ);

            player.Translate((attractForce * Time.deltaTime * moveDirection));
        }
        else
            player.parent = null;
    }



}
