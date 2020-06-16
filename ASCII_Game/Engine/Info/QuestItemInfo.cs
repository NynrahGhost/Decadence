public class QuestItemInfo : ItemInfo
{
    public QuestItemInfo(string name, ushort weight, string description, ushort price) : 
        base(name, weight, description, price)
    {
    }
    
    public override string[] GetInfo()
    {
        return new[]{"Name: " + GetName(),
            "Description: " + GetDescription(),
            "Weight: " + GetWeight(),
            "Price: " + GetPrice()};
    }
}
