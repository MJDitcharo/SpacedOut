using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmmoCountBase : MonoBehaviour
{
    [SerializeField]
    public int Quantity { get; set; } = 0;
    [SerializeField]
    TMPro.TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        UpdateVisual();
    }

    private void Update()
    {
        //check if ammo count changed
        //update ammo count depending on which ammo count was changed
    }

    //public void OnWeaponSwitch()
    //change the ammo count

    protected void UpdateVisual()
    {
        textMeshPro.text = Quantity.ToString();
    }

    protected void SubtractAmmo(int ammo = 0)
    {
        if (ammo == 0)
            Quantity--;
        else
            Quantity -= ammo;
        UpdateVisual();
    }
    protected void AddAmmo(int ammo = 0)
    {
        if (ammo == 0)
            Quantity++;
        else
            Quantity += ammo;
        UpdateVisual();
    }

    public virtual void SetAmmoCount(int ammo)
    {
        Quantity = ammo;
        UpdateVisual();
    }

}
