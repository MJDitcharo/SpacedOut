using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    //uses the base Update function
    private void Awake()
    {
        weaponID = WeaponID.Pistol;
    }
}
