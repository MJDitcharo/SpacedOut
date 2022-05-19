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
    List<Pickups> chestContents;

    List<int> chestQuantities;
    [SerializeField]
    int health, ammo, skrap, grenade, boardWipe;

    List<Pickups> chestContents2;

    List<Drop> playerRewards;

    List<int> playerCounts;
    bool chestOpened = false;


    // Start is called before the first frame update
    void Start()
    {
        pLight = GetComponent<Light>(); //get light component in the box
        FillChest();
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
        AddPlayerItems();

        //turn off prompt and start animation
        GameManager.instance.prompt.HidePrompt();
        defaultVec = crateTop.position;
        playAni = true;
        chestOpened = true;

        //turn on chest visual
        GameManager.instance.chestUI.Activate(chestContents);

        ShowQuantityChange();
        //reward contents
        RewardContents();
    }

    private void AddPlayerItems()
    {
        //get items player has
        //should only grab the types inside of chestContents
        playerCounts.Add(GameManager.instance.ammoCount.GetQuantity());
        playerCounts.Add(GameManager.instance.skrapCount.GetQuantity());
        playerCounts.Add(GameManager.instance.grenadeCount.GetQuantity());
        playerCounts.Add(GameManager.instance.boardWipeCount.GetQuantity());
        playerCounts.Add(GameManager.instance.healthBar.GetHealthInt());
        //store the quantity of the specified items the player has in playerCounts
    }

    private void FillChest()
    {
        playerRewards.Add(new Drop(ammo, "Ammo", playerCounts[(int)Rewards.Ammo]));
        playerRewards.Add(new Drop(skrap, "Skrap", playerCounts[(int)Rewards.Skrap]));
        playerRewards.Add(new Drop(grenade, "Grenade", playerCounts[(int)Rewards.Grenade]));
        playerRewards.Add(new Drop(boardWipe, "Board Wipe", playerCounts[(int)Rewards.BoardWipe]));
        playerRewards.Add(new Drop(health, "Health", playerCounts[(int)Rewards.Health]));
    }

    private void RewardContents()
    {
        GameManager.instance.ammoCount.Add(ammo);
        GameManager.instance.skrapCount.Add(skrap);
        GameManager.instance.grenadeCount.Add(grenade);
        GameManager.instance.boardWipeCount.Add(boardWipe);
        GameManager.instance.healthBar.AddHealth(health);
    }

    private void ShowQuantityChange()
    {
        int slotCount = 0;
        foreach(int rewards in Enum.GetValues(typeof(Rewards)))
        {
            if (playerCounts[(int)rewards] + chestQuantities[(int)rewards] != 0)
                AddToSlots(ref slotCount, (Rewards)rewards);
        }
    }

    private void AddToSlots(ref int slotCount, Rewards reward)
    {
        GameManager.instance.chestUI.SetText("Slot " + slotCount.ToString(), playerRewards[(int)reward].ItemName);
        slotCount++;

    }

}
