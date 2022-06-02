using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemCount : MonoBehaviour
{
    [SerializeField]
    int quantity = 0;
    [SerializeField]
    private int max = 0;
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
    public void Subtract(int ammo = 0)
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
    public void Add(int ammo = System.Int32.MaxValue)
    {
        if (ammo == System.Int32.MaxValue)
            quantity++;
        else if (ammo + quantity > max)
        {
            quantity = max;
        }
        else
            quantity += ammo;
        UpdateVisual();
    }

    public virtual void SetMaximumQuatnity(int ammo)
    {
        max = ammo;
        UpdateVisual();
    }

    public virtual void SetQuantity(int ammo)
    {
        if (ammo >= 0)
            quantity = ammo;
        UpdateVisual();
    }
    public int GetQuantity()
    {
        return quantity;
    }
    public int GetMaximumQuantity()
    {
        return max;
    }


}
