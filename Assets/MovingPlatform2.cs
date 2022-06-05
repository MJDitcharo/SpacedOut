using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform2 : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed;
    public int target;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime);    
    }

    private void FixedUpdate()
    {
        if(transform.position == waypoints[target].position)
        {
            if (target == waypoints.Count - 1)
                target = 0;
            else
                target += 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            Debug.Log("enter");
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            Debug.Log("exit");
            other.transform.parent = null;
        }
    }

}
