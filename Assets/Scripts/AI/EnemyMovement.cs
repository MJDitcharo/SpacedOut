using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent navmeshAgent;
    GameObject player;
    public float movementSpeed;

    void Start()
    {
        player = GameManager.instance.player;
    }

    public void MoveToLocation(Vector3 position)
    {
        Debug.Log("moving to " + position);
        navmeshAgent.SetDestination(position);
    }
}
