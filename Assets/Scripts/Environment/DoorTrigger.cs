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


    //Vector3 sliderLTransform;
    //Vector3 sliderRTransform;

    //Vector3 openPositionL;
    //Vector3 closedPositionL;
    //Vector3 openPositionR;
    //Vector3 closedPositionR;


    //how far the door panels open
    const float doorMovement = 1f;

    bool doorOpen = false;
    private void Awake()
    {
        //sliderLTransform = doorSliderL.transform.position;
        //sliderRTransform = doorSliderR.transform.position;

        //closedPositionL = sliderLTransform;
        //openPositionL = new Vector3(sliderLTransform.x - doorMovement, sliderLTransform.y, sliderLTransform.z);

        //closedPositionR = sliderRTransform;
        //openPositionR= new Vector3(sliderRTransform.x + doorMovement, sliderRTransform.y, sliderRTransform.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        //opendoor
        if (other.gameObject == GameManager.instance.player)
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
        
        Debug.Log("Door Will Close");

        doorSliderL.SetActive(true);
        doorSliderR.SetActive(true);
        //doorSliderL.transform.position = closedPositionL;
        //doorSliderR.transform.position = closedPositionR;

        doorOpen = false;

    }
    private void OpenDoor()
    {
        Debug.Log("Door will open");

        doorSliderL.SetActive(false);
        doorSliderR.SetActive(false);
        //doorSliderL.transform.position = openPositionL;
        //doorSliderR.transform.position = openPositionR;

        doorOpen = false;
    }

    //see which side of the door the player came from
    private void CheckSide()
    {

    }

}