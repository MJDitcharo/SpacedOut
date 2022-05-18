using System.Collections;
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
    [SerializeField]
    List<Pickups> chestContents;

    List<int> chestQuantities;
    [SerializeField]
    int health, ammo, skrap, grenade, boardWipe;
    
    List<int> playerCounts;
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
            crateTop.position = Vector3.Lerp(crateTop.position, defaultVec + offset,time); //mpve the top of the box 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!chestOpened)
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
        GameManager.instance.prompt.HidePrompt();
        defaultVec = crateTop.position;
        playAni = true;
        chestOpened = true;

        //turn on chest visual
        GameManager.instance.chestUI.Activate(chestContents);

        for (int i = 0; i < chestContents.Count; i++)
        {
            string objName = "Slot " + (i + 1);
            GameManager.instance.chestUI.SetText(objName, chestContents[i].GetItemString());
        }
        //reward contents
    }

    private void PlayerItems()
    {
        //get items player has
        //should only grab the types inside of chestContents
        //store the quantity of the specified items the player has in playerCounts
    }

    private void FillChest()
    {
        //instantiate the items in chest contents
        //spawn them just below the player
            //copy the player's x and z, make the y lower than the player
        //get the quantities of the chestContents
    }

    private void GiveChestContents()
    {
        //change the y value of the instantiated items to match the player's
    }

    private void ShwoQuantityChange()
    {
        //FillChest
        //PlayerItems
        //build a string with the change in quantites
            //ex: Ammo x 10 ~ 34 -> 44
              //player quantites^  ^new player quantites
    }

    
}
