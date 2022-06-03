using UnityEngine;

public class WeaponUpgradePage : StorePage
{
    // Start is called before the first frame update
    bool weaponOwned; //see if player has bought the weapon
    [SerializeField]
    public int ammoCost, ammoQuantity, fireRateCost, fireRateQuantity, damageCost, damageQuantity, tier2Upgrade1, tier2Upgrade2, voidUpgrade, plasma;
    [SerializeField]
    protected GameObject buyAmmo, upgrade1, upgrade2, upgrade3;

    

    protected override void SetInitialPrices()
    {
        pricesInt.Add(ammoCost);
        pricesInt.Add(fireRateCost);
        pricesInt.Add(damageCost);
        pricesInt.Add(tier2Upgrade1);
        pricesInt.Add(tier2Upgrade2);
        pricesInt.Add(voidUpgrade);
        pricesInt.Add(plasma);
    }

    protected void UpgradeMeshes(GameObject upgrade)
    {
        bool isFirst = true;
        foreach (Transform level1 in upgrade.transform)
        {
            if (isFirst) //in the Grid Layout Group, the "Tier" textmeshpro is always first
            {
                isFirst = false;
                continue;
            }
            priceText.Add(level1.Find("Price Icon").transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>());
        }
    }

    protected override void AssignTextMeshes()
    {
        //add the ammo first
        priceText.Add(buyAmmo.transform.Find("Price Icon").transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>());
        UpgradeMeshes(upgrade1);
        UpgradeMeshes(upgrade2);
        UpgradeMeshes(upgrade3);

    }

}
