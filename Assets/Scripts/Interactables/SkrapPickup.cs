using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrapPickup : Pickups
{
    protected override void Increment()
    {
        GameManager.instance.skrapCount.Add(quantity); 
    }
    protected override void InitialValues()
    {
        drop.ItemName = "Skrap x " + quantity.ToString();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            //give the player bullets
            Increment();
            SkrapPopUpManager.Instance.StartCoroutine(SkrapPopUpManager.Instance.SkrapIndicator(quantity));
            Destroy(gameObject);
        }
    }
}
