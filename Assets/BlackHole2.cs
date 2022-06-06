using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole2 : MonoBehaviour
{

    [SerializeField]
    float attractForce, attractRange, moveX, moveZ;
    Vector3 moveDirection;
    Transform player;
    PlayerMovement pMove;
    State[] enemies;
     public bool isPullable;
    private void Start()
    {
        player = GameManager.instance.player.transform;
        pMove = GameManager.instance.player.GetComponent<PlayerMovement>();
        enemies = FindObjectsOfType<State>();
    }


    private void FixedUpdate()
    {
        if (player.CompareTag(tag) && isPullable == true)
        {
            if (Vector3.Distance(player.position, transform.position) < attractRange) //check to see if player is in range
            {

                moveDirection = gameObject.transform.position;

                pMove.pushback = moveDirection - player.position;
                player.Translate(attractForce * Time.deltaTime * pMove.pushback);

            }
            else
                player.parent = null;
        }
        else
        {
            foreach (State enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < attractRange) //check to see if player is in range
                {

                    moveDirection = gameObject.transform.position;

                    enemy.movement.pushback = moveDirection - enemy.transform.position;
                    enemy.transform.Translate(attractForce* Time.deltaTime * enemy.movement.pushback);

                }
                else
                    player.parent = null;
            }
        }
        
    }

}
