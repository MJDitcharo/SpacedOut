using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePage : MonoBehaviour
{

    //class prerequisites
    //must assign set initial prices
    //must assign gridFormat

    [SerializeField]
    protected GameObject gridFormat;
    protected List<int> pricesInt = new();
    protected List<TMPro.TextMeshProUGUI> priceText = new();

    protected void Start()
    {
        SetDefaultStatus();
    }


    protected void AssignTextMeshes()
    {
        foreach (Transform level1 in gridFormat.transform)
        {
            Debug.Log(level1.name);
            priceText.Add(level1.Find("Price Icon").transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>());
        }
        Debug.Log(priceText.Count);

        for (int i = 0; i < priceText.Count; i++)
            priceText[i].text = pricesInt[i].ToString();
    }

    protected void SetDefaultStatus()
    {
        //assign grid format
        SetInitialPrices();
        AssignTextMeshes();
        //set the correct buttons to be active in hierarchy
    }


    /// <summary>
    /// Pure Virtual.
    /// </summary>
    protected virtual void SetInitialPrices()
    {

    }
}
