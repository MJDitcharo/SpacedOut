using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePage : MonoBehaviour
{

    //class prerequisites
    //must assign set initial prices
    //must assign gridFormat

    
    protected List<int> pricesInt = new();
    protected List<TMPro.TextMeshProUGUI> pricesText = new();

    protected virtual void Start()
    {
        SetDefaultStatus();
    }

    /// <summary>
    /// Pure Virtual.
    /// </summary>
    protected virtual void SetInitialPrices()
    {
        Debug.Log("Need to define function: SetInitialPrices");
    }

    /// <summary>
    /// Pure Virtual.
    /// </summary>
    protected virtual void AssignTextMeshes()
    {
        Debug.Log("Need to define function: AssignTextMeshes");
    }

    protected virtual void SetTextMeshPrices()
    {
        for (int i = 0; i < pricesText.Count; i++)
        {
            Debug.Log(pricesText[i].transform.parent.name + " num is :" + pricesInt[i].ToString());
            pricesText[i].text = pricesInt[i].ToString();
        }
    }

    protected void SetDefaultStatus()
    {
        //assign grid format
        SetInitialPrices();
        AssignTextMeshes();
        SetTextMeshPrices();
        //set the correct buttons to be active in hierarchy
    }


    
}
