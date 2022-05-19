using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]bool inStore = false;
    // Update is called once per frames
    private void OnTriggerEnter(Collider other)
    {
            GameManager.instance.prompt.ShowPrompt("Press F To Enter");
    }

    private void OnTriggerStay(Collider other)
    {
        //open the chest
        if (!inStore)
        {
            if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F))
            {
                EnterStore();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inStore = false;
        GameManager.instance.prompt.HidePrompt();
    }

    private void EnterStore()
    {
        GameManager.instance.prompt.HidePrompt();
        inStore = true;
        //GameManager.instance.storeUI.Activate();
    }

}
