public interface IItem
{
    int ItemId { get; }
    bool Sellable { get; }
    ushort Count { get; }
    ItemType Type { get; }

    public bool SellItem();
    public bool SellItems(int number);
    public bool GiveItem();
    public bool UseItem();
    public string GetName();
    public string GetInfo();
}
