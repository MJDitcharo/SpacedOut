using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : PopUpMenu
{
    [SerializeField]
    private GameObject shopVisual;
    public TMPro.TextMeshProUGUI purchaseMessage;
    [HideInInspector]
    public GameObject purchaseMessageObj;
    [SerializeField]
    private GameObject[] pages;
    bool first = true;
    //singleton
    public static UIStore instance;

    //pickup page

    private void Start()
    {
        if (instance == null)
            instance = this;
        purchaseMessageObj = purchaseMessage.gameObject;

        foreach(GameObject game in pages)
        {
            if (first)
            {
                game.SetActive(true);
                first = false;
            }
            else
                game.SetActive(false);
        }
        Deactivate(); //should be off by default
    }
    public void Activate()
    {
        if (!GameManager.instance.pmenu.gameIsPaused)
        {
            shopVisual.SetActive(true);
            GameManager.instance.menuIsActive = true;
            //GameObject.Find("Weapon Upgrades Page").gameObject.SetActive(false);
            FreezeWorld();
        }
    }
    public void Deactivate()
    {
        shopVisual.SetActive(false);
        GameManager.instance.menuIsActive = false;
        UnfreezeWorld();
    }


}