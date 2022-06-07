using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCloneBullet : bullet
{
    [SerializeField] GameObject shadowClone;

    public override void OnTriggerEnter(Collider other)
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);

        health hp = other.GetComponent<health>();
        if (hp != null)
        {
            if(hp.currHealth - damage <= 0)
            {
                Instantiate(shadowClone, other.transform.position, Quaternion.identity);
            }
        }
        base.OnTriggerEnter(other);
    }
}
