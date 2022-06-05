using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPage : StorePage
{
    [SerializeField]
    protected GameObject gridFormat;
    public int healthCost, maxHealth, grenade, boardWipe;
    public int healthQuantity;
    public static GeneralPage instance;

    private void Awake()
    {
        instance = this;
        healthQuantity = 25;
    }

    protected override void AssignTextMeshes()
    {
        foreach (Transform level1 in gridFormat.transform)
            pricesText.Add(level1.Find("Price Icon").transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>());
    }

    protected override void SetInitialPrices()
    {
        pricesInt.Add(healthCost);
        pricesInt.Add(maxHealth);
        pricesInt.Add(grenade);
        pricesInt.Add(boardWipe);
    }


}