using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickups
{
    // Start is called before the first frame update
    private void Awake()
    {
        Quantity = 20;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //give the player bullets
            GameManager.instance.ammoCount.AddAmmo(20);
            Destroy(gameObject);
        }
    }
}
