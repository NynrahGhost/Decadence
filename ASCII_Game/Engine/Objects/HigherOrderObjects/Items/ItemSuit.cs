using System;

public class ItemSuit : IItem
{
    public uint ItemId { get { return ItemId; } set { ItemId = value; } }
    public bool Sellable { get { return Sellable; } set { Sellable = value; } }
    public ushort Count { get { return Count; } set { Count = value; } }

    public ItemType Type { get { return Type; } set { Type = value; } }

    public ItemSuit(uint itemId, bool sellable, ushort count = 1)
    {
        ItemId = itemId;
        Sellable = sellable;
        Count = count;
        Type = ItemType.Suit;
    }

    public string[] GetInfo()
    {
        return GameItemsInfo.SuitItems[ItemId].GetInfo();
    }
    public string GetName()
    {
        return GameItemsInfo.SuitItems[ItemId].GetName();
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
