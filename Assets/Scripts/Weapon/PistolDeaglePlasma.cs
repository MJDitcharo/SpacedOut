using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDeaglePlasma : bullet
{
    public int bulletAmount = 3;
    [SerializeField] GameObject splitBullet;

    public override void OnTriggerEnter(Collider other)
    {
        //for (int i = 0; i < bulletAmount; i++)
        //{
            Debug.Log("here");
            Instantiate(splitBullet, transform).GetComponent<ChildSplitBullet>().ignore = other;
            GameObject bullet = transform.GetChild(0).gameObject;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.transform.SetParent(null);

            rb.AddForce(transform.forward, ForceMode.Impulse); 
        //}

        base.OnTriggerEnter(other);
    }
}
