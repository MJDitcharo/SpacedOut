using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole2 : MonoBehaviour
{

    [SerializeField]
    float attractForce, attractRange = 10;
    Transform objectToMove;
    PlayerMovement controller;


    private void Start()
    {
        controller = GameManager.instance.player.GetComponent<PlayerMovement>();
    }
    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        //if(Vector3.Distance(other.transform.position, transform.position) < attractRange)
        if (other.gameObject == GameManager.instance.player)
        {
            Debug.Log("enter. old pos" + other.transform.position);
            //other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, attractForce * Time.deltaTime);
            Debug.Log("new pos" + other.transform.position);
        }
    }
}
