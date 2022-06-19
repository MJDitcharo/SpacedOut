using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    //number of items this will give the player
    [SerializeField]
    public int quantity;
    protected Drop drop = new Drop();
    private void Start()
    {
        drop.Quantity = quantity;
        InitialValues();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            //give the player bullets
            Increment();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Put whatever needs to be incremented in here.
    /// </summary>
    protected abstract void Increment();
    /// <summary>
    /// Must assign drop.itemStr. This method will be called on Start()
    /// </summary>
    protected abstract void InitialValues();
    public string GetItemName()
    {
        return drop.ItemName;
    }
}