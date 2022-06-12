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
    protected GameObject buyAmmo;
    [SerializeField]
    protected List<GameObject> upgradeTiers = new();
    protected string tier2Choice = "";
    protected WeaponBase.WeaponID pageID;
    protected int currentTier = 0;
    private int maxTier;
    protected string pageName; //MUST be set in the start function for each child. This is an abstract field
    bool firstLoad = true;

    protected override void Start()
    {
        base.Start();
        maxTier = upgradeTiers.Count;
        TiersFromPlayerPrefs(pageName);
    }

    private void OnEnable()
    {
        if(gameObject.activeInHierarchy)
        {
            Debug.Log("Switching");
            WeaponHolder.instance.SwitchWeapons(pageID);
            firstLoad = false;
        }
    }
    protected override void SetInitialPrices()
    {
        pricesInt.Add(ammoCost);
        if (upgradeTiers[0] != null)
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
            pricesText.Add(level1.Find("Price Icon").transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>());
        }
    }

    protected override void SetTextMeshPrices()
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
        pricesText.Add(buyAmmo.transform.Find("Price Icon").transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>());
        pricesText.Add(upgradeTiers[0].transform.Find("Price Icon").transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>());
        UpgradeMeshes(upgradeTiers[1]);
        UpgradeMeshes(upgradeTiers[2]);
        UpgradeMeshes(upgradeTiers[3]);

    }

    protected void CheckUnlock(string weaponName)
    {
        if (WeaponHolder.instance.IsWeaponUnlocked(weaponName))
        {
            //set tier 1 upgrade and ammo true, buy weapon to false
            upgradeTiers[0].SetActive(false);
            buyAmmo.SetActive(true);
        }
        
    }

    public void NextTier()
    {
        upgradeTiers[currentTier].transform.position += new Vector3(2500, 0, 0);
        if (currentTier == 0)
            buyAmmo.SetActive(true);
        currentTier++;
        if (currentTier < maxTier)
            upgradeTiers[currentTier].SetActive(true);
    }

    public void SetTier2Choice(string _tier2Choice)
    {
        tier2Choice = _tier2Choice;
    }

    public string GetTeir2Choice()
    {
        return tier2Choice;
    }

    public int GetCurrentTier()
    {
        return currentTier;
    }

    protected void TiersFromPlayerPrefs(string pageName)
    {
        for (int i = 0; i < PlayerPrefs.GetInt(pageName); i++)
            NextTier();
    }

}
