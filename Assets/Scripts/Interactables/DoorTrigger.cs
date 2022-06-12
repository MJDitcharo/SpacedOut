using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    BoxCollider doorCollider;
    [SerializeField]
    GameObject doorSliderL;
    [SerializeField]
    GameObject doorSliderR;
    [SerializeField]
    GameObject lights;
    public bool closed { private get; set; }

    //Vector3 sliderLTransform;
    //Vector3 sliderRTransform;

    //Vector3 openPositionL;
    //Vector3 closedPositionL;
    //Vector3 openPositionR;
    //Vector3 closedPositionR;


    //how far the door panels open
    const float doorMovement = 1f;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //opendoor
        if (other.gameObject == GameManager.instance.player && !closed)
        {
            //check if room is clear of enemies
            OpenDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            CloseDoor();
        }
    }
    private void CloseDoor()
    {
        doorSliderL.SetActive(true);
        doorSliderR.SetActive(true);

    }
    private void OpenDoor()
    {
        doorSliderL.SetActive(false);
        doorSliderR.SetActive(false);
        //turn on the lights
        if (lights != null)
            lights.SetActive(false);

    }
}
