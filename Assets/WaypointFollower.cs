using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    GameObject[] waypoints;
    int target;
    [SerializeField]
    float speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, waypoints[target].transform.position ) < 1f)
        {
            target++;
            if (target >= waypoints.Length)
                target = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[target].transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            Debug.Log("Enter");
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            Debug.Log("exti");
            collision.gameObject.transform.SetParent(null);
        }
    }
}
