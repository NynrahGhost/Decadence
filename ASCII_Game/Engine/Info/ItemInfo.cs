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

    public ref readonly string GetName() { return ref name; }
    public ref readonly ushort GetWeight() { return ref weight; }
    public ref readonly string GetDescription() { return ref description; }
    public ref readonly ushort GetPrice() { return ref price; }
}
