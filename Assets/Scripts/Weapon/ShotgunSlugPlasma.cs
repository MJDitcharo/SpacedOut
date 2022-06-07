using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSlugPlasma : Shotgun
{

    public int projectileSpeed;
    // Start is called before the first frame update
    public override void Shoot()
    {
        GameObject blast = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        blast.transform.rotation = transform.rotation;
        Rigidbody blastRB = blast.GetComponent<Rigidbody>();
        blastRB.velocity = transform.forward * projectileSpeed;
        AudioManager.Instance.PlaySFX("baseGun");
    }
}
