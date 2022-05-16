using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    //number of items this will give the player
    [SerializeField]
    protected int quantity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            Debug.Log("hit");
            //give the player bullets
            ItemToIncrement();
            Destroy(gameObject);
        }
    }

    protected abstract void ItemToIncrement();
}