using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientMovingPlatform : MonoBehaviour
{
    [SerializeField]
    GameObject[] waypoints;
    int target;
    [SerializeField]
    float speed;
    bool passenger;
    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (passenger)
            MovePlatform();
        if (GameManager.instance.gameOverScreen.activeInHierarchy) //if the player died
            GameManager.instance.player.transform.SetParent(null);
    }

    protected void MovePlatform()
    {
        if (Vector3.Distance(transform.position, waypoints[target].transform.position) < 1f)
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
            passenger = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            Debug.Log("exti");
            collision.gameObject.transform.SetParent(null);
            passenger = false;
        }
    }
}
