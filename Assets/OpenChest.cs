using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] Transform crateTop;
    Light pLight;
    bool playAni = false;
    Vector3 defaultVec;
    [SerializeField] Vector3 offset;
    [SerializeField] float time = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        pLight = GetComponent<Light>(); //get light component in the box
    }

    // Update is called once per frame
    void Update()
    {
        if (playAni)
        {
            crateTop.position = Vector3.Lerp(crateTop.position, defaultVec + offset,time); //mpve the top of the box 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playAni) //only move if colliding eith the player and do it once 
        {
            defaultVec = crateTop.position;
            playAni = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pLight.enabled = false; //disable light after exiting trigger
        
    }

}
