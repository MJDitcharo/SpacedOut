using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyPage : WeaponUpgradePage
{
    static public HeavyPage instance;
    protected override void Start()
    {
        instance = this;
        base.Start();
        CheckUnlock("Heavy");
    }
}
