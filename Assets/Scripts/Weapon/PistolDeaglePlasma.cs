using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDeaglePlasma : bullet
{
    public int bulletAmount = 3;
    [SerializeField] GameObject splitBullet;
    [SerializeField] float splitForce = 20;
    public Collider ignore;

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        transform.Rotate(0, -30, 0);
        for (float i = -30; i <= 30; i += 30)
        {
            Instantiate(splitBullet, transform).GetComponent<ChildSplitBullet>().ignore = other;
            GameObject bullet = transform.GetChild(0).gameObject;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.transform.SetParent(null);

            rb.AddForce(transform.forward * splitForce, ForceMode.Impulse); 
            transform.Rotate(0, 30, 0);
        }
        //Debug.Break();

        base.OnTriggerEnter(other);
    }
}
