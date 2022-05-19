using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop
{

    public int Quantity { get; set; }
    public string ItemName { get; set; }

    public Drop()
    {

    }

    /// <summary>
    /// Used for Chests. Gives ItemName the following format:
    ///     ItemName = mItemName + " x " + Quantity + " " + oldCount + " -> " + newCount;
    /// </summary>
    /// <param name="mQuantity"></param>
    /// <param name="mItemName"></param>
    /// <param name="oldCount"></param>
    public Drop(int mQuantity, int max, string mItemName, int oldCount)
    {
        Quantity = mQuantity;
        int newCount = Helper.AddInt(Quantity, oldCount, max);

        ItemName = mItemName + " x " + Quantity + "\t" + oldCount + " -> " + newCount;
    }
}
