using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] RoomManager roomManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        roomManager.LockDownRoom();
    }
}
