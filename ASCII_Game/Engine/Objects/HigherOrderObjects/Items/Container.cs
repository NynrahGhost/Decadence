using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Container
{
    private uint MaxWeight { get { return MaxWeight; } set { this.MaxWeight = value; } }
    private uint CurrentWeight
    { 
        get { return CurrentWeight; } 
        set { if ((value + CurrentWeight) <= MaxWeight) CurrentWeight += value; } 
    }

    private Dictionary<uint, IItem> Suits = new Dictionary<uint, IItem>();
    private Dictionary<uint, IItem> Weapons = new Dictionary<uint, IItem>();
    private Dictionary<uint, IItem> Medicines = new Dictionary<uint, IItem>();
    private Dictionary<uint, IItem> Other = new Dictionary<uint, IItem>();

    public Container(uint maxWeight)
    {
        this.MaxWeight = maxWeight;
        CurrentWeight = 0;
    }

    public bool AddItem(IItem item)
    {
        return false;
    }

    //public int AddItems(IItem item, ushort number) { }
    public bool RemoveItem(uint itemId, ushort count = 1, bool isSell = true)
    {
        return false;
    }
}
