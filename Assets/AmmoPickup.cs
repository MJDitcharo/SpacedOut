using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickups
{
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player)
        {
            Debug.Log("hit");
            //give the player bullets
            GameManager.instance.ammoCount.AddAmmo(quantity);
            Destroy(gameObject);
        }
    }


}
