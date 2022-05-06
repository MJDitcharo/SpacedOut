using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    BoxCollider doorCollider;
    private int doorRoomId = 1;
    bool doorOpen;

    private void OnTriggerExit(Collider other)
    {
        //when the player is done touching the trigger, close the door
        if (other.gameObject == GameManager.instance.player)
            CloseDoor();
    }

    private void CloseDoor()
    {
        Debug.Log("Door Will Close");
    }
    private void OpenDoor()
    {
        Debug.Log("Door will open");
    }

}
