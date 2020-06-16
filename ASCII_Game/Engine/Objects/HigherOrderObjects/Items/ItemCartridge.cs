class ItemCartridge : IItem
{
    public uint ItemId { get { return ItemId; } set { ItemId = value; } }
    public bool Sellable { get { return Sellable; } set { Sellable = value; } }
    public ushort Count { get { return Count; } set { Count = value; } }

    public ItemType Type { get { return Type; } set { Type = value; } }

    public ItemCartridge(uint itemId, bool sellable, ItemType type, ushort count = 1)
    {
        ItemId = itemId;
        Sellable = sellable;
        Type = type;
        Count = count;
    }

    public string[] GetInfo()
    {
        return GameItemsInfo.CartridgeItems[ItemId].GetInfo();
    }
    public string GetName()
    {
        return GameItemsInfo.CartridgeItems[ItemId].GetName();
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
