using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyPage : WeaponUpgradePage
{
    static public HeavyPage instance;

    private void Awake()
    {
        pageID = WeaponBase.WeaponID.Heavy;
    }

    protected override void Start()
    {
        instance = this;
        pageName = "HeavyPage";
        base.Start();
        CheckUnlock("Heavy");
    }
}
