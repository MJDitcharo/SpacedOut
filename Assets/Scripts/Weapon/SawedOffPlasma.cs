using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawedOffPlasma : bullet
{

    public override void OnTriggerEnter(Collider other)
    {
        health hp = other.GetComponent<health>();
        if(hp != null)
            hp.burnTimer = 5;
        base.OnTriggerEnter(other);

    }

}
