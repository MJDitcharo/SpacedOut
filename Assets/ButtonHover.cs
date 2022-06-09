using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    string weaponDescription;
    [SerializeField]
    string tier2WeaponName;
    [SerializeField]
    bool tier3, isPlasma;
    string tier3Name;
    bool element1;
    [SerializeField]
    WeaponBase.WeaponID weaponID;
    private void Start()
    {
        if (isPlasma)
            tier3Name = " Plasma";
        else
            tier3Name = " Void";
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //see where to move the description
        if (tier3)
        {
            string name = WeaponHolder.instance.GetEquippedWeaponName(weaponID);
            weaponDescription = WeaponHolder.instance.GetWeaponDescription(name + tier3Name);
        }
        else 
            weaponDescription = WeaponHolder.instance.GetWeaponDescription(tier2WeaponName);
        UIStore.instance.descriptionText.text = weaponDescription;
        UIStore.instance.weaponDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIStore.instance.weaponDescription.SetActive(false);
    }

}
