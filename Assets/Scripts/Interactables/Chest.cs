using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    enum Rewards
    {
        Ammo,
        Skrap,
        Grenade,
        BoardWipe,
        Health
    };

    [SerializeField]
    Transform crateTop;
    Light pLight;
    bool playAni = false;
    Vector3 defaultVec;
    [SerializeField] Vector3 offset;
    [SerializeField] float time = 0.01f;
    [SerializeField]
    int health, ammo, skrap, grenade, boardWipe;

    List<Drop> playerRewards = new();

    List<int> playerItems = new();
    bool chestOpened = false;


    //convenience variables
    ItemCount grenadeCountInst;
    ItemCount ammoCountInst;
    ItemCount boardWipeInst;
    ItemCount skrapCountInst;
    HealthBar healthBarInst;
    playerHealth playerHealthInst;

    // Start is called before the first frame update
    void Start()
    {
        pLight = GetComponent<Light>(); //get light component in the box

        grenadeCountInst = GameManager.instance.grenadeCount;
        ammoCountInst = GameManager.instance.ammoCount;
        boardWipeInst = GameManager.instance.boardWipeCount;
        skrapCountInst = GameManager.instance.skrapCount;
        healthBarInst = GameManager.instance.healthBar;
        playerHealthInst = GameManager.instance.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playAni)
            crateTop.position = Vector3.Lerp(crateTop.position, defaultVec + offset, time); //mpve the top of the box 
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        //open the chest
        if (other.gameObject.CompareTag("Player") && !playAni && Input.GetKey(KeyCode.F)) //only move if colliding eith the player and do it once 
            OpenChest();
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.instance.prompt.HidePrompt();
        pLight.enabled = false; //disable light after exiting trigger
    }

    private void OpenChest()
    {
        //grab the player's current items
        GetPlayerItems();
        //fill chest contents
        FillChest();

        //turn off prompt and start animation
        GameManager.instance.prompt.HidePrompt();
        defaultVec = crateTop.position;
        playAni = true;
        chestOpened = true;

        //reward contents and SetText
        RewardContents();

        //turn on chest visual
        GameManager.instance.chestUI.Activate();

    }

    private void GetPlayerItems()
    {
        //get current items player has
        //should only grab the types inside of chestContents
        playerItems.Add(ammoCountInst.GetQuantity());
        playerItems.Add(skrapCountInst.GetQuantity());
        playerItems.Add(grenadeCountInst.GetQuantity());
        playerItems.Add(boardWipeInst.GetQuantity());
        playerItems.Add(healthBarInst.GetHealthInt());
        //store the quantity of the specified items the player has in playerCounts
    }

    private void FillChest()
    {
        //set rewards, fill the chestQuantities list
        playerRewards.Add(new Drop(ammo, ammoCountInst.GetMaximumQuantity(), "Ammo", playerItems[(int)Rewards.Ammo]));
        playerRewards.Add(new Drop(skrap, skrapCountInst.GetMaximumQuantity(), "Skrap", playerItems[(int)Rewards.Skrap]));
        playerRewards.Add(new Drop(grenade, grenadeCountInst.GetMaximumQuantity(), "Grenade", playerItems[(int)Rewards.Grenade]));
        playerRewards.Add(new Drop(boardWipe, boardWipeInst.GetMaximumQuantity(), "Board Wipe", playerItems[(int)Rewards.BoardWipe]));
        playerRewards.Add(new Drop(health, healthBarInst.GetMaxHealth(), "Health", playerItems[(int)Rewards.Health]));
    }

    private void RewardContents()
    {
        WeaponBase weaponToUpdate = null; //weapon to update the visual
        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            WeaponBase currentWeapon = WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>();
            currentWeapon.ammoCount += ammo;
            if (currentWeapon.ammoCount > currentWeapon.maxAmmo)
                currentWeapon.ammoCount = currentWeapon.maxAmmo;
            if (currentWeapon.isActiveAndEnabled)
                weaponToUpdate = currentWeapon;
        }

        if (weaponToUpdate != null)
            weaponToUpdate.UpdateVisual();
        //ammoCountInst.Add(ammo);
        skrapCountInst.Add(skrap);
        grenadeCountInst.Add(grenade);
        boardWipeInst.Add(boardWipe);
        healthBarInst.AddHealth((float)(health * .01f));
        ShowQuantityChange();
    }

    private void ShowQuantityChange()
    {
        foreach (int rewards in Enum.GetValues(typeof(Rewards)))
        {
            if (playerItems[(int)rewards] + playerRewards[(int)rewards].Quantity != playerItems[(int)rewards])
                AddToSlots((Rewards)rewards);
        }
    }

    private void AddToSlots(Rewards reward)
    {
        GameManager.instance.chestUI.SetText(playerRewards[(int)reward].ItemName);
    }

}
