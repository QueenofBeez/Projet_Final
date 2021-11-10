using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : HotbarItem
{
    [Header("Item Data")]
    [Min(0)] private int sellPrice = 1;
    [Min(1)] private int maxStack = 1;

    public override string ColouredName // { get {return Name; }}
    {
        get{
            return Name;
        }
    }
    public int SellPrice => sellPrice; // {get {return sellPrice;} } 
    public int MaxStack => maxStack; // {get {return maxStack;} } 
}
