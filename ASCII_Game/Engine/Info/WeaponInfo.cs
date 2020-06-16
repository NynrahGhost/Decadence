public class WeaponInfo : ItemInfo
{
    private readonly uint CartridgeId;
    private readonly uint Damage;
    private readonly long Cooldown;

    public WeaponInfo(string name, ushort weight, string description, ushort price, 
        uint carteidgeId, uint damage, long cooldown) :
        base(name, weight, description, price)
    {
        CartridgeId = carteidgeId;
        Damage = damage;
        Cooldown = cooldown;
    }

    public uint GetCartridgeID() { return CartridgeId; }
    public uint GetDamage() { return Damage; }
    public long GetCooldown() { return Cooldown; }

    public override string[] GetInfo() 
    {
        return new[]{"Name: " + GetName(),
            "Description: " + GetDescription(),
            "Weight: " + GetWeight(),
            "Price: " + GetPrice(),
            "Used cartridges: " + GameItemsInfo.CartridgeItems[CartridgeId].GetName(),
            "Damage: " + Damage,
            "Cooldown time: " + Cooldown};
    }
}
