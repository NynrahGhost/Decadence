public class WeaponInfo : ItemInfo
{
    private readonly uint Damage;
    private readonly long Cooldown;

    public WeaponInfo(string name, ushort weight, string description, ushort price, 
        uint damage, long cooldown) :
        base(name, weight, description, price)
    {
        Damage = damage;
        Cooldown = cooldown;
    }

    public ref readonly uint GetDamage() { return ref Damage; }
    public ref readonly long GetCooldown() { return ref Cooldown; }
}
