using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPage : WeaponUpgradePage
{
    static public ShotgunPage instance;
    protected override void Start()
    {
        instance = this;
        base.Start();
        //check if the weapon is unlocked
        CheckUnlock("Shotgun");
        TiersFromPlayerPrefs("Pistol Page");
    }



}
