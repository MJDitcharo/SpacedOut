using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBullets : bullet
{
    public override void OnTriggerEnter(Collider other)
    {
        health hp = other.gameObject.GetComponent<health>();
        if(hp != null)
        {
            if (hp.stun == null)
                hp.StunMethod();
        }
        
        base.OnTriggerEnter(other);
    }
}
