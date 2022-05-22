using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPouch : MonoBehaviour
{
    [SerializeField]
    GameObject grenade;
    [SerializeField]
    GameObject boardWipe;
    float throwForce = 20;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ThrowBoardWipe();
        else if (Input.GetKeyDown(KeyCode.Q))
            ThrowGrenade();
    }

    private void ThrowItem(GameObject itemToThrow)
    {
        Instantiate(itemToThrow, transform.position, Quaternion.identity);
        Rigidbody body = grenade.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        body.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }

    private void ThrowGrenade()
    {
        ThrowItem(grenade);

    }

    private void ThrowBoardWipe()
    {
        ThrowItem(boardWipe);
    }

}
