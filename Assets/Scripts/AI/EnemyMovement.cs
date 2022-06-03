using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent navmeshAgent;
    [SerializeField] float pushbackFalloffSpeed = 4;
    public Vector3 pushback;

    GameObject player;
    public float movementSpeed;

    void Start()
    {
        player = GameManager.instance.player;
        pushback = new Vector3(pushback.x, 0, pushback.z);
    }
    private void Update()
    {
        pushback = Vector3.Lerp(pushback, Vector3.zero, pushbackFalloffSpeed * Time.deltaTime);
        navmeshAgent.Move(movementSpeed * Time.deltaTime * pushback);
    }
    public void MoveToLocation(Vector3 position)
    {
        navmeshAgent.SetDestination(position);
    }
    public NavMeshAgent GetAgent()
    {
        return navmeshAgent;
    }
}
