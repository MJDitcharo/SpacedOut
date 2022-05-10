using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    BoxCollider doorCollider;
    [SerializeField]
    GameObject doorSlider;
    private int doorRoomId = 1;
    bool doorOpen = true;

    private void OnTriggerExit(Collider other)
    {
        //when the player is done touching the trigger, close the door
        if (other.gameObject == GameManager.instance.player)
        {
            if (doorOpen)
                CloseDoor();
            else
                OpenDoor();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player)
        {
            //check if the room is cleared
        }
    }

    private void CloseDoor()
    {
        Debug.Log("Door Will Close");
        
        doorSlider.transform.position += new Vector3(0, 2, 0);
        doorOpen = false;

    }
    private void OpenDoor()
    {
        Debug.Log("Door will open");
    }

}
