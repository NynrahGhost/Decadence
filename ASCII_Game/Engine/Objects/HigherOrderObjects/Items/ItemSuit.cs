using System;

public class ItemSuit : IItem
{
    public int ItemId { get { return ItemId; } set { ItemId = value; } }
    public bool Sellable { get { return Sellable; } set { Sellable = value; } }
    public ushort Count { get { return Count; } set { Count = value; } }

    public ItemType Type { get { return Type; } set { Type = value; } }

    public ItemSuit(int itemId, bool sellable, ItemType type, ushort count = 1)
    {
        ItemId = itemId;
        Sellable = sellable;
        Type = type;
        Count = count;
    }

    public string GetInfo()
    {
        return "";
    }
    public string GetName()
    {
        return "";
    }

    public bool GiveItem()
    {
        return false;
    }
    public bool SellItem()
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
