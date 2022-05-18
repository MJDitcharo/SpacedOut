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

        //turn on chest visual
        GameManager.instance.chestUI.Activate(chestContents);
        //pour out contents
        //for (int i = 0; i < GameManager.instance.chestUI.GetChildCount(); i++)
        //{
        //    string objName = "Slot " + (i + 1);
        //    GameManager.instance.chestUI.SetText(objName, string.Empty);
        //    //print GetText for every item in ChestContents
        //    //print to each slot in ChestSlots
        //    if (i == chestContents.Count)
        //        break;
        //}

        for (int i = 0; i < chestContents.Count; i++)
        {
            string objName = "Slot " + (i + 1);
            GameManager.instance.chestUI.SetText(objName, chestContents[i].GetString());
        }
        chestOpened = true;
    }

}
