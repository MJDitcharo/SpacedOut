using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : WeaponBase
{
    // Start is called before the first frame update
    private void Awake()
    {
        weaponID = WeaponID.Shotgun;
    }
}
