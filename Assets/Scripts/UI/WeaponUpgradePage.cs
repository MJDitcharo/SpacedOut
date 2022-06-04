using UnityEngine;

public class WeaponUpgradePage : StorePage
{
    // Start is called before the first frame update
    bool weaponOwned; //see if player has bought the weapon
    [SerializeField]
    public int ammoCost, ammoQuantity, tier1Upgrade, tier2Upgrade, tier3Upgrade;
    [SerializeField]
    public float fireRateQuantity, damageQuantity;
    [SerializeField]
    protected GameObject buyAmmo, upgrade1, upgrade2, upgrade3;



    protected override void SetInitialPrices()
    {
        pricesInt.Add(ammoCost);
        pricesInt.Add(tier1Upgrade);
        pricesInt.Add(tier2Upgrade);
        pricesInt.Add(tier3Upgrade);
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
            pricesText.Add(level1.Find("Price Icon").transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>());
        }
    }

    protected override void SetTextMeshPrices()
    {
        int j = 0; //variable for going throug
        for (int i = 0; i < pricesText.Count; i++)
        {
            if (i == 0) //the ammo count will always be first, avoid div by 0 error
            {
                pricesText[i].text = pricesInt[i].ToString();
                j++;
                continue;
            }
            float temp = (float)i / j;
            //j = Mathf.roun(temp);
            if(temp - Mathf.RoundToInt(temp) != 0) //round up to the next integer
            {
                temp += .5f;
            }
            j = (int)temp;
            pricesText[i].text = pricesInt[j].ToString();
            if(temp == 0)
            {
                j++;
            }
        }
        Debug.Log(pricesText[6].transform.parent.transform.parent.name);
    }

    protected override void AssignTextMeshes()
    {
        //add the ammo first
        pricesText.Add(buyAmmo.transform.Find("Price Icon").transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>());
        UpgradeMeshes(upgrade1);
        UpgradeMeshes(upgrade2);
        UpgradeMeshes(upgrade3);

    }

}
