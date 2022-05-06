using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    //grab the player 
    GameObject player = GameManager.instance.player;
    [SerializeField]
    BoxCollider doorCollider;
    private void FixedUpdate()
    {
        //if the player collides with the doortrigger

    }
}
