public class ItemInfo
{
    private readonly string name;
    private readonly ushort weight;
    private readonly string description;
    private readonly ushort price;

    public ItemInfo(string name, ushort weight, string description, ushort price)
    {
        this.name = name;
        this.weight = weight;
        this.description = description;
        this.price = price;
    }

    public string GetName() { return name; }
    public ushort GetWeight() { return weight; }
    public string GetDescription() { return description; }
    public ushort GetPrice() { return price; }
    
    public virtual string[] GetInfo()
    {
        return new[] {"Name: " + name,
            "Description: " + description,
            "Weight: " + weight,
            "Price: " + price };
    }
}
