using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField]
    Transform crateTop;
    Light pLight;
    bool playAni = false;
    Vector3 defaultVec;
    [SerializeField] Vector3 offset;
    [SerializeField] float time = 0.01f;
    public int associatedCheckpoint = 0;
    public int skrap;
    int oldSkrapCount;

    bool chestOpened = false;


    //convenience variables
    ItemCount skrapCountInst;

    // Start is called before the first frame update
    void Start()
    {
        pLight = GetComponent<Light>(); //get light component in the box

        skrapCountInst = GameManager.instance.skrapCount;

        defaultVec = crateTop.position;

        if (associatedCheckpoint == PlayerPrefs.GetInt("Checkpoint Index") && PlayerPrefs.GetInt("Chest Opened") == 1)
        {
            playAni = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playAni)
            crateTop.position = Vector3.Lerp(crateTop.position, defaultVec + offset, time); //mpve the top of the box 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playAni) //only move if colliding eith the player and do it once 
            GameManager.instance.prompt.ShowPrompt("Press F to Enter");
    }

    private void OnTriggerStay(Collider other)
    {
        //open the chest
        if (other.gameObject.CompareTag("Player") && !playAni && Input.GetKey(KeyCode.F)) //only move if colliding eith the player and do it once 
            OpenChest();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //only move if colliding eith the player and do it once 
        {
            GameManager.instance.prompt.HidePrompt();
            pLight.enabled = false; //disable light after exiting trigger

        }
    }

    private void OpenChest()
    {
        //grab the player's current items
        GetPlayerItems();
        //fill chest contents

        //turn off prompt and start animation
        GameManager.instance.prompt.HidePrompt();
        //defaultVec = crateTop.position;
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
        oldSkrapCount = skrapCountInst.GetQuantity();
    }


    private void RewardContents()
    {
        skrapCountInst.Add(skrap);
        AddToSlots();
        GameManager.instance.SaveGame();
        PlayerPrefs.SetInt("Chest Opened", 1);
    }
    private void AddToSlots()
    {
        GameManager.instance.chestUI.SetText("Skrap x " + skrap + "   " + oldSkrapCount + " -> " + skrapCountInst.GetQuantity());
    }

}
