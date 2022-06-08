using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPage : WeaponUpgradePage
{
    static public ShotgunPage instance;
    protected override void Start()
    {
        instance = this;
        pageName = "ShotgunPage";
        base.Start();
        CheckUnlock("Shotgun");
    }



}
