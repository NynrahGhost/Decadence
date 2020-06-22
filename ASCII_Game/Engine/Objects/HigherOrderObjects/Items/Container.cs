using System.Collections.Generic;

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
        MaxWeight = maxWeight;
        CurrentWeight = 0;
    }

    public bool AddItem(IItem item)
    {
        switch (item.Type)
        {
            case ItemType.Cartridge:
                ushort weight = GameItemsInfo.CartridgeItems[item.ItemId].GetWeight();
                if ((MaxWeight - CurrentWeight) < (weight*item.Count))
                {
                    return false;
                }
                else
                {
                    CurrentWeight = (uint)(CurrentWeight + weight * item.Count);
                    if (Weapons.ContainsKey(item.ItemId)) { Weapons[item.ItemId].Count += item.Count; }
                    else { Weapons.Add(item.ItemId, new ItemCartridge(item.ItemId, item.Sellable, item.Count)); }
                    return true;
                }
            case ItemType.Medicine:
                weight = GameItemsInfo.MedicineItems[item.ItemId].GetWeight();
                if ((MaxWeight - CurrentWeight) < (weight * item.Count))
                {
                    return false;
                }
                else
                {
                    CurrentWeight = (uint)(CurrentWeight + weight * item.Count);
                    if (Medicines.ContainsKey(item.ItemId)) { Weapons[item.ItemId].Count += item.Count; }
                    else { Medicines.Add(item.ItemId, new ItemMedicine(item.ItemId, item.Sellable, item.Count)); }
                    return true;
                }
            case ItemType.Quest:
                weight = GameItemsInfo.QuestItems[item.ItemId].GetWeight();
                if ((MaxWeight - CurrentWeight) < (weight * item.Count))
                {
                    return false;
                }
                else
                {
                    CurrentWeight = (uint)(CurrentWeight + weight * item.Count);
                    if (Other.ContainsKey(item.ItemId)) { Weapons[item.ItemId].Count += item.Count; }
                    else { Other.Add(item.ItemId, new ItemQuest(item.ItemId, item.Sellable, item.Count)); }
                    return true;
                }
            case ItemType.Simple:
                weight = GameItemsInfo.SimpleItems[item.ItemId].GetWeight();
                if ((MaxWeight - CurrentWeight) < (weight * item.Count))
                {
                    return false;
                }
                else
                {
                    CurrentWeight = (uint)(CurrentWeight + weight * item.Count);
                    if (Other.ContainsKey(item.ItemId)) { Weapons[item.ItemId].Count += item.Count; }
                    else { Other.Add(item.ItemId, new ItemSimple(item.ItemId, item.Sellable, item.Count));}
                    return true;
                }
            case ItemType.Suit:
                weight = GameItemsInfo.SuitItems[item.ItemId].GetWeight();
                if ((MaxWeight - CurrentWeight) < (weight * item.Count))
                {
                    return false;
                }
                else
                {
                    CurrentWeight = (uint)(CurrentWeight + weight * item.Count);
                    if (Suits.ContainsKey(item.ItemId)) { Weapons[item.ItemId].Count += item.Count; }
                    else { Suits.Add(item.ItemId, new ItemSuit(item.ItemId, item.Sellable, item.Count)); }
                    return true;
                }
            case ItemType.Weapon:
                weight = GameItemsInfo.WeaponItems[item.ItemId].GetWeight();
                if ((MaxWeight - CurrentWeight) < (weight * item.Count))
                {
                    return false;
                }
                else
                {
                    CurrentWeight = (uint)(CurrentWeight + weight * item.Count);
                    if (Weapons.ContainsKey(item.ItemId)) { Weapons[item.ItemId].Count += item.Count; }
                    else { Weapons.Add(item.ItemId, new ItemWeapon(item.ItemId, item.Sellable, item.Count)); }
                    return true;
                }
        }
        return false;
    }
    public bool RemoveItem(IItem item, ushort count = 1, bool isSell = true)
    {
        if (item.Type == ItemType.Cartridge || item.Type == ItemType.Weapon)
        {
            if (Weapons.ContainsKey(item.ItemId) && item.Count >= count)
            {
                if (isSell) { item.SellItems(count); }
                else { item.GiveItems(count); }
                if (item.Count > count) { item.Count -= count; }
                else { Weapons.Remove(item.ItemId); }
                return true;
            }
            else { return false; }
        }
        else if (item.Type == ItemType.Suit)
        {
            if (Suits.ContainsKey(item.ItemId) && item.Count >= count)
            {
                if (isSell) { item.SellItems(count); }
                else { item.GiveItems(count); }
                if (item.Count > count) { item.Count -= count; }
                else { Weapons.Remove(item.ItemId); }
                return true;
            }
            else { return false; }
        }
        else if (item.Type == ItemType.Medicine)
        {
            if (Medicines.ContainsKey(item.ItemId) && item.Count >= count)
            {
                if (isSell) { item.SellItems(count); }
                else { item.GiveItems(count); }
                if (item.Count > count) { item.Count -= count; }
                else { Weapons.Remove(item.ItemId); }
                return true;
            }
            else { return false; }
        }
        else
        {
            if (Other.ContainsKey(item.ItemId) && item.Count >= count)
            {
                if (isSell) { item.SellItems(count); }
                else { item.GiveItems(count); }
                if (item.Count > count) { item.Count -= count; }
                else { Weapons.Remove(item.ItemId); }
                return true;
            }
            else { return false; }
        }
    }
}
