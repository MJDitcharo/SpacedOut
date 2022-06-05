using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : PopUpMenu
{
    [SerializeField]
    private GameObject shopVisual;
    [SerializeField]
    public GameObject purchaseMessageObj;
    [SerializeField]
    private GameObject firstPage;
    bool first = true;
    public TMPro.TextMeshProUGUI purchaseMessage;
    //singleton
    public static UIStore instance;

    //pickup page

    private void Start()
    {
        if (instance == null)
            instance = this;
        Debug.Log(transform.name);
        //if (shopVisual == null)
        //    shopVisual = OneShopVisual;
        //if (purchaseMessageObj == null)
        //    purchaseMessageObj = statPurchaseMessage;
        //else
        //    statPurchaseMessage = purchaseMessageObj;

        //grab textmesh component for purchase message
        purchaseMessage = purchaseMessageObj.GetComponent<TMPro.TextMeshProUGUI>();

        //new pages system
        //foreach (Transform page in shopVisual.transform)
        //{
        //    if (page.name.Contains("Page"))
        //    {
        //        pages.Add(page.gameObject);
        //        if (first) //the first page in the hierarchy will be the only one shown
        //        {
        //            page.gameObject.SetActive(true);
        //            first = false;
        //        }
        //        else
        //            page.gameObject.SetActive(false);
        //    }
        //}
        if (first)
        {
            firstPage.SetActive(true);
            first = false;
        }

    }
    public void Activate()
    {
        if (!GameManager.instance.pmenu.gameIsPaused)
        {
            shopVisual.SetActive(true);
            GameManager.instance.menuIsActive = true;
            //GameObject.Find("Weapon Upgrades Page").gameObject.SetActive(false);
            FreezeWorld();
            Debug.Log("menu is active");
        }
    }
    public void Deactivate()
    {
        shopVisual.SetActive(false);
        GameManager.instance.menuIsActive = false;
        UnfreezeWorld();
        Debug.Log("menu is deactive");

    }

   
}