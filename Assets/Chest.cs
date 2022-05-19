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


    // Start is called before the first frame update
    void Start()
    {
        pLight = GetComponent<Light>(); //get light component in the box
    }

    // Update is called once per frame
    void Update()
    {
        if (playAni)
        {
            crateTop.position = Vector3.Lerp(crateTop.position, defaultVec + offset, time); //mpve the top of the box 
        }
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (!chestOpened)
            GameManager.instance.prompt.ShowPrompt("Press F To Open");
    }

    private void OnTriggerStay(Collider other)
    {
        //open the chest
        if (other.gameObject.CompareTag("Player") && !playAni && Input.GetKeyDown(KeyCode.F)) //only move if colliding eith the player and do it once 
            OpenChest();
    }

    private void OnTriggerExit(Collider other)
    {
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
        playerItems.Add(GameManager.instance.ammoCount.GetQuantity());
        playerItems.Add(GameManager.instance.skrapCount.GetQuantity());
        playerItems.Add(GameManager.instance.grenadeCount.GetQuantity());
        playerItems.Add(GameManager.instance.boardWipeCount.GetQuantity());
        playerItems.Add(GameManager.instance.healthBar.GetHealthInt());
        //store the quantity of the specified items the player has in playerCounts
    }

    private void FillChest()
    {
        //set rewards, fill the chestQuantities list
        playerRewards.Add(new Drop(ammo, "Ammo", playerItems[(int)Rewards.Ammo]));
        playerRewards.Add(new Drop(skrap, "Skrap", playerItems[(int)Rewards.Skrap]));
        playerRewards.Add(new Drop(grenade, "Grenade", playerItems[(int)Rewards.Grenade]));
        playerRewards.Add(new Drop(boardWipe, "Board Wipe", playerItems[(int)Rewards.BoardWipe]));
        playerRewards.Add(new Drop(health, "Health", playerItems[(int)Rewards.Health]));
    }

    private void RewardContents()
    {
        GameManager.instance.ammoCount.Add(ammo);
        GameManager.instance.skrapCount.Add(skrap);
        GameManager.instance.grenadeCount.Add(grenade);
        GameManager.instance.boardWipeCount.Add(boardWipe);
        GameManager.instance.healthBar.AddHealth(health);
        ShowQuantityChange();
    }

    private void ShowQuantityChange()
    {
        int slotCount = 1;
        foreach(int rewards in Enum.GetValues(typeof(Rewards)))
        {
            if (playerItems[(int)rewards] + playerRewards[(int)rewards].Quantity != 0)
                AddToSlots(ref slotCount, (Rewards)rewards);
        }
    }

    private void AddToSlots(ref int slotCount, Rewards reward)
    {
        GameManager.instance.chestUI.SetText("Slot " + slotCount.ToString(), playerRewards[(int)reward].ItemName);
        slotCount++;

    }

}
