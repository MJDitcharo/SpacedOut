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

    protected void UpdateVisual()
    {
        textMeshPro.text = Quantity.ToString();
    }
    /// <summary>
    /// Subtract the specified ammo. Subtracts 1 bullet by default
    /// </summary>
    /// <param name="ammo"></param>
    public void SubtractAmmo(int ammo = 0)
    {
        if (ammo == 0)
            Quantity--;
        else
            Quantity -= ammo;
        UpdateVisual();
    }

    /// <summary>
    /// Adds the specified ammo. Adds 1 bullet by default
    /// </summary>
    /// <param name="ammo"></param>
    public void AddAmmo(int ammo = 0)
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
