using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    //number of items this will give the player
    [SerializeField]
    protected int quantity;
    protected string itemStr;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            Debug.Log("hit");
            //give the player bullets
            Increment();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Put whatever needs to be changed in here.
    /// </summary>
    protected abstract void Increment();
    public string GetItemString()
    {
        return itemStr;
    }
}