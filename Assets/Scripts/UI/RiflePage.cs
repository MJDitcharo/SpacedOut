using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePage : WeaponUpgradePage
{
    static public RiflePage instance;
    protected override void Start()
    {
        instance = this;
        base.Start();
        CheckUnlock("Rifle");
    }
}
