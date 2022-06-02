using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePage : MonoBehaviour
{
    [SerializeField]
    protected List<int> pricesInt;
    protected List<TMPro.TextMeshProUGUI> priceText;
    protected GameObject gridFormat;
    void Start()
    {   
        SetDefaultStatus();
    }

   

    protected void SetDefaultStatus()
    {
        //assign grid format
        foreach (Transform level1 in gridFormat.transform)
        {
            foreach (Transform level2 in level1)
            {
                Debug.Log(level2.name);
            }
        }
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
