using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawedOffPlasma : bullet
{

    public override void OnTriggerEnter(Collider other)
    {
        other.GetComponent<health>().burnTimer = 5;
        base.OnTriggerEnter(other);

    }

}
