using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmmoCountBase : MonoBehaviour
{
    [SerializeField]
    int quantity;
    [SerializeField]
    TMPro.TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        GetAmmoCount();
        textMeshPro.text = quantity.ToString();

    }

    private void Update()
    {
        //check if ammo count changed
        //update ammo count depending on which ammo count was changed
    }

    //public void OnWeaponSwitch()
    //change the ammo count

    private void UpdateAmmoCount()
    {

    }

    protected virtual void GetAmmoCount()
    {

    }

}
