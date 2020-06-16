﻿

public class ItemWeapon : IItem
{
    public uint ItemId { get { return ItemId; } set { ItemId = value; } }
    public bool Sellable { get { return Sellable; } set { Sellable = value; } }
    public ushort Count { get { return Count; } set { Count = value; } }

    public ItemType Type { get { return Type; } set { Type = value; } }

    public ItemWeapon(uint itemId, bool sellable, ItemType type, ushort count = 1)
    {
        ItemId = itemId;
        Sellable = sellable;
        Type = type;
        Count = count;
    }

    public string[] GetInfo()
    {
        return GameItemsInfo.WeaponItems[ItemId].GetInfo();
    }
    public string GetName()
    {
        return GameItemsInfo.WeaponItems[ItemId].GetName();
    }

    public bool GiveItems(int number)
    {
        return false;
    }
    public bool SellItems(int number)
    {
        return false;
    }
    public bool UseItem()
    {
        return false;
    }
}
