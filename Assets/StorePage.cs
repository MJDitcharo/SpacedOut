using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePage : MonoBehaviour
{
    [SerializeField]
    protected GameObject gridFormat;
    protected List<int> pricesInt;
    protected List<TMPro.TextMeshProUGUI> priceText;
    void Start()
    {   
        SetDefaultStatus();
    }

   

    protected void GetTextMeshes()
    {
        foreach (Transform level1 in gridFormat.transform)
        {
            foreach (Transform level2 in level1)
                priceText.Add(level2.Find("Price").GetComponent<TMPro.TextMeshProUGUI>());
        }
    }

    protected void SetDefaultStatus()
    {
        //assign grid format
        GetTextMeshes();
        SetInitialPrices();
        //set the correct buttons to be active in hierarchy
    }

    protected virtual void SetInitialPrices()
    {
        //needs to change the number variable AND the textmeshpro

    }

    protected void AssignTextMeshes()
    {

    }
}
