using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    string weaponDescription;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        UIStore.instance.weaponDescription.SetActive(false);
    }

}
