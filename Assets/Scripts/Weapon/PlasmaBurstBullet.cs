using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBurstBullet : bullet
{
    [SerializeField] float increasedDamageIncrease = .25f;
    public override void OnTriggerEnter(Collider other)
    {
        health hp = other.GetComponent<health>();
        
        if(hp != null && hp.increasedDamage <= 2.75f)
        {
            hp.increasedDamage += increasedDamageIncrease;
        }
        
        damage = (int)(hp.increasedDamage * damage);
        base.OnTriggerEnter(other);
    }
}
