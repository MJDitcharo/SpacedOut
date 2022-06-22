using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    bool inStore = false;
    [SerializeField] string prompt = "Press F To Build";
    // Update is called once per frames
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameManager.instance.prompt.ShowPrompt(prompt);
    }

    private void OnTriggerStay(Collider other)
    {
        //open the chest
        if (other.gameObject.CompareTag("Player"))
        {
            if (!GameManager.instance.prompt.gameObject.activeInHierarchy)
                GameManager.instance.prompt.ShowPrompt(prompt);
            if (Input.GetKey(KeyCode.F))
                EnterStore();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inStore = false;
            GameManager.instance.prompt.HidePrompt();
        }

    }

    private void EnterStore()
    {
        GameManager.instance.prompt.HidePrompt();
        inStore = true;
        GameManager.instance.shopUI.Activate();
    }

}
