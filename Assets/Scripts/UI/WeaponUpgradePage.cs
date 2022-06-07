using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradePage : StorePage
{
    // Start is called before the first frame update
    bool weaponOwned; //see if player has bought the weapon
    [SerializeField]
    public int ammoCost, weaponCost, ammoQuantity, tier1Upgrade, tier2Upgrade, tier3Upgrade;
    [SerializeField]
    public float fireRateQuantity, damageQuantity;
    [SerializeField]
    protected GameObject buyAmmo, buyWeapon;
    [SerializeField]
    protected List<GameObject> upgradeTiers = new();
    protected int currentTier = 0;
    private const int maxTier = 2;
    protected string weaponName; //MUST be set in the start function for each child. This is an abstract field

    protected override void Start()
    {
        base.Start();
    }
    protected override void SetInitialPrices()
    {
        pricesInt.Add(ammoCost);
        if (buyWeapon != null)
            pricesInt.Add(weaponCost);
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
        if (buyWeapon != null)
        {
            SetTextMeshPricesWithWeapon();
        }
        else
        {
            int j = 0; //variable for going throug
            for (int i = 0; i < pricesText.Count; i++)
            {
                string currentName = pricesText[i].transform.parent.transform.parent.name;
                if (currentName == "Buy Ammo" || currentName == "Buy Weapon") //the ammo count will always be first, avoid div by 0 error
                {
                    pricesText[i].text = pricesInt[i].ToString();
                    j++;
                    continue;
                }
                if (currentName == "Plasma")
                {
                    int a = 0;
                }
                float temp = (float)i / j;
                //j = Mathf.roun(temp);
                if (temp - Mathf.RoundToInt(temp) != 0) //round up to the next integer
                {
                    temp += .5f;
                }
                if (i == 2) // 2 will need to be 1, this is a special case
                    temp = 1;
                else if (i == 3)
                    temp = 2;
                else if (i == 6)
                    temp = 3;

                j = (int)temp;
                pricesText[i].text = pricesInt[j].ToString();
                if (temp == 0)
                {
                    j++;
                }
                //Debug.Log(pricesText[i].transform.parent.transform.parent.name);
            }
        }
    }

    private void SetTextMeshPricesWithWeapon()
    {
        pricesText[0].text = pricesInt[0].ToString();
        pricesText[1].text = pricesInt[1].ToString();
        pricesText[2].text = pricesInt[2].ToString();
        pricesText[3].text = pricesInt[2].ToString();
        pricesText[4].text = pricesInt[3].ToString();
        pricesText[5].text = pricesInt[3].ToString();
        pricesText[6].text = pricesInt[4].ToString();
        pricesText[7].text = pricesInt[4].ToString();
    }

    protected override void AssignTextMeshes()
    {
        //add the ammo first
        pricesText.Add(buyAmmo.transform.Find("Price Icon").transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>());
        if (buyWeapon != null)
            pricesText.Add(buyWeapon.transform.Find("Price Icon").transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>());
        UpgradeMeshes(upgradeTiers[0]);
        UpgradeMeshes(upgradeTiers[1]);
        UpgradeMeshes(upgradeTiers[2]);

    }

    protected void CheckUnlock(string weaponName)
    {
        if (WeaponHolder.instance.IsWeaponUnlocked(weaponName))
        {
            //set tier 1 upgrade and ammo true, buy weapon to false
            upgradeTiers[0].SetActive(true);
            buyAmmo.SetActive(true);
            if (buyWeapon != null)
                buyWeapon.SetActive(false);
        }
        else
        {
            upgradeTiers[0].SetActive(false);
            buyAmmo.SetActive(false);
            buyWeapon.SetActive(true);
        }
    }
    public void FirstTier()
    {
        buyWeapon.transform.position += new Vector3(30, 0, 0);
        buyAmmo.SetActive(true);
        upgradeTiers[0].SetActive(true);
    }

    public void NextTier()
    {
        if (currentTier + 1 <= maxTier)
        {
            //upgradeTiers[currentTier].SetActive(false);
            upgradeTiers[currentTier].transform.position += new Vector3(30, 0, 0); 
            currentTier++;
            upgradeTiers[currentTier].SetActive(true);
        }
    }
}
