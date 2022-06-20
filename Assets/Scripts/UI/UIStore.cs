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
    public GameObject weaponDescription;
    [HideInInspector]
    public TMPro.TextMeshProUGUI descriptionText;
    public Vector3 descriptionPos1, descriptionPos2;
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
        descriptionText = weaponDescription.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        weaponDescription.SetActive(false);
        foreach (GameObject game in pages)
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


    public IEnumerator HandlePurchaseMessage(bool purchaseFailed, string message = "")
    {
        if (!purchaseFailed)
        {
            UIStore.instance.purchaseMessage.color = Color.green;
            message = "Transaction Success!";
        }
        else
        {
            UIStore.instance.purchaseMessage.color = Color.red;
            if (message == "")
                message = "Not Enough Skrap!";
        }
        UIStore.instance.purchaseMessage.text = message;
        UIStore.instance.purchaseMessageObj.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        UIStore.instance.purchaseMessageObj.SetActive(false);
    }


    public void StartMessageCoroutine(bool purchaseFailed = true, string message = "")
    {
        StartCoroutine(HandlePurchaseMessage(purchaseFailed, message)); 
    }

    public void Activate()
    {
        if (!GameManager.instance.pmenu.gameIsPaused)
        {
            GameManager.instance.SetNormalCursor();
            shopVisual.SetActive(true);
            GameManager.instance.shopIsActive = true;
            FreezeWorld();
        }
    }
    public void Deactivate()
    {
        GameManager.instance.SetFightingCursor();
        shopVisual.SetActive(false);
        GameManager.instance.shopIsActive = false;
        if (purchaseMessageObj.activeInHierarchy)
            purchaseMessageObj.SetActive(false);
        UnfreezeWorld();
    }


}