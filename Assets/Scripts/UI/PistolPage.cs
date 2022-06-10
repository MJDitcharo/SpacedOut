using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolPage : WeaponUpgradePage
{
    static public PistolPage instance;
    private void Awake()
    {
        pageID = WeaponBase.WeaponID.Pistol;
    }
    protected override void Start()
    {
        instance = this;
        pageName = "PistolPage";
        base.Start();
        CheckUnlock("Pistol");
    }
}
