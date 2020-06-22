class MonsterInfo
{
    private readonly string Name;
    private readonly string Description;
    private readonly ushort Health;
    private readonly byte Agility;
    private readonly byte Accuracy;
    private readonly byte Resistance;
    private readonly byte Level;

    private readonly uint EffectId;
    private readonly float EffectProbability;

    private readonly ushort Money;

    public MonsterInfo(string name, string description, ushort health, byte agility, byte accuracy, byte resistance, 
        byte level, ushort money, uint effectId, float effectProbability)
    {
        Name = name;
        Description = description;
        Health = health;
        Agility = agility;
        Accuracy = accuracy;
        Resistance = resistance;
        Level = level;

        EffectId = effectId;
        EffectProbability = effectProbability;

        Money = money;
    }

    public string GetName() { return Name; }
    public string GetDescription() { return Description; }
    public ushort GetHealth() { return Health; }
    public byte GetAgility() { return Agility; }
    public byte GetAccuracy() { return Accuracy; }
    public byte GetResistance() { return Resistance; }
    public byte GetLevel() { return Level; }
    public uint GetEffectId() { return EffectId; }
    public float GetEffectProbability() { return EffectProbability; }
    public ushort GetMoney() { return Money; }
}
