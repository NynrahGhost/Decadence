using System.Linq;

public class WeaponInfo : ItemInfo
{
    private readonly uint CartridgeId;
    private readonly uint Damage;
    private readonly long Cooldown;
    private readonly uint EffectId;
    private readonly float EffectProbability;

    public WeaponInfo(string name, ushort weight, string description, ushort price, 
        uint carteidgeId, uint damage, long cooldown, uint effectId, float effectProbability) :
        base(name, weight, description, price)
    {
        CartridgeId = carteidgeId;
        Damage = damage;
        Cooldown = cooldown;
        EffectId = effectId;
        EffectProbability = effectProbability;
    }

    public uint GetCartridgeId() { return CartridgeId; }
    public uint GetDamage() { return Damage; }
    public long GetCooldown() { return Cooldown; }
    public uint GetEffectId() { return EffectId; }
    public float GetEffectProbability() { return EffectProbability; }

    public override string[] GetInfo() 
    {
        return (new[]{"Name: " + GetName(),
            "Description: " + GetDescription(),
            "Weight: " + GetWeight(),
            "Price: " + GetPrice(),
            "Used cartridges: " + GameItemsInfo.CartridgeItems[CartridgeId].GetName(),
            "Damage: " + Damage,
            "Cooldown time: " + Cooldown,
            "Effect probablility: " + EffectProbability,
            "Effect: "}).Concat(GameItemsInfo.Effects[EffectId].GetInfo()).ToArray();
    }
}
