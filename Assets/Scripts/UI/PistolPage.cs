using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolPage : WeaponUpgradePage
{
    static public PistolPage instance;
    protected override void Start()
    {
        instance = this;
        base.Start();
        CheckUnlock("Pistol");
    }

}
