using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePage : WeaponUpgradePage
{
    static public RiflePage instance;
    private void Awake()
    {
        pageID = WeaponBase.WeaponID.Rifle;
    }
    protected override void Start()
    {
        instance = this;
        pageName = "RiflePage";
        base.Start();
        CheckUnlock("Rifle");
    }
}
