using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    string weaponDescription;
    private void Start()
    {
        /*if (isPlasma)
            tier3Name = " Plasma";
        else
            tier3Name = " Void";*/
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //see where to move the description
        UIStore.instance.descriptionText.text = weaponDescription;
        UIStore.instance.weaponDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIStore.instance.weaponDescription.SetActive(false);
    }

}
