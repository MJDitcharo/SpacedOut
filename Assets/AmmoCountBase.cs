using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmmoCountBase : MonoBehaviour
{
    [SerializeField]
    int quantity = 0;
    int maxAmmo = 0;
    //int maxAmmo;
    [SerializeField]
    TMPro.TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        UpdateVisual();
    }

    protected void UpdateVisual()
    {
        textMeshPro.text = quantity.ToString();
    }

    /// <summary>
    /// Subtract the specified ammo. Subtracts 1 bullet by default
    /// </summary>
    /// <param name="ammo"></param>
    public void SubtractAmmo(int ammo = 0)
    {
        if (ammo == 0)
            quantity--;
        else if (quantity - ammo < 0)
            quantity = 0;
        else
            quantity -= ammo;
        UpdateVisual();
    }

    /// <summary>
    /// Adds the specified ammo. Adds 1 bullet by default
    /// </summary>
    /// <param name="ammo"></param>
    public void AddAmmo(int ammo = 0)
    {
        if (ammo == 0)
            quantity++;
        else if (ammo + quantity > maxAmmo)
            quantity = maxAmmo;
        else
            quantity += ammo;
        UpdateVisual();
    }

    public virtual void SetAmmoCount(int ammo)
    {
        if (ammo >= 0)
            quantity = ammo;
        UpdateVisual();
    }

    public virtual void SetMaxAmmoCount(int ammo)
    {
        maxAmmo = ammo;
        UpdateVisual();
    }

}
