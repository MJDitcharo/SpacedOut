using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStoreButtons : MonoBehaviour
{
    public void PreviousPage()
    {
        for (int i = 0; i < UIStore.instance.pages.Count; i++)
        {
            if (UIStore.instance.pages[i].activeInHierarchy)
            {
                //deactivate current page
                UIStore.instance.pages[i].SetActive(false);//set the next page as active.
                if (i - 1 < 0)
                    UIStore.instance.pages[UIStore.instance.pages.Count - 1].SetActive(true);
                else
                    UIStore.instance.pages[i - 1].SetActive(true);
            }
        }
    }
    public void NextPage()
    {
        Debug.Log(UIStore.instance.pages.Count);
        for (int i = 0; i < UIStore.instance.pages.Count; i++)
        {
            if (UIStore.instance.pages[i].activeInHierarchy)
            {
                //deactivate current page
                UIStore.instance.pages[i].SetActive(false);
                if (i + 1 < UIStore.instance.pages.Count)//set the next page as active.
                    UIStore.instance.pages[i + 1].SetActive(true);
                else //if the next is out of bounds, go to the first page
                    UIStore.instance.pages[0].SetActive(true);
                break;
            }
        }
    }


}
