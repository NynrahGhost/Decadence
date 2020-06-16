public interface IItem
{
    uint ItemId { get; }
    bool Sellable { get; }
    ushort Count { get; set; }
    ItemType Type { get; }

    public bool SellItems(int number = 1);
    public bool GiveItems(int number = 1);
    public bool UseItem();
    public string GetName();
    public string[] GetInfo();
}
