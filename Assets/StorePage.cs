using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePage : MonoBehaviour
{
    [SerializeField]
    protected GameObject gridFormat;
    protected List<int> pricesInt;
    protected List<TMPro.TextMeshProUGUI> priceText = new();

    protected virtual void Start()
    {
        SetDefaultStatus();
    }


    protected void AssignTextMeshes()
    {
        foreach (Transform level1 in gridFormat.transform)
        {
            priceText.Add(level1.Find("Price").GetComponent<TMPro.TextMeshProUGUI>());
        }
        for (int i = 0; i < priceText.Count; i++)
        {
            priceText[i].text = pricesInt[i].ToString();
        }
    }

    protected void SetDefaultStatus()
    {
        //assign grid format
        AssignTextMeshes();
        SetInitialPrices();
        //set the correct buttons to be active in hierarchy
    }


    /// <summary>
    /// Pure Virtual.
    /// </summary>
    protected virtual void SetInitialPrices()
    {

    }
}
