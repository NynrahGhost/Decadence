class MonsterInfo
{
    private readonly string name;
    private readonly string description;
    private readonly ushort health;
    private readonly byte agility;
    private readonly byte accuracy;
    private readonly byte resistance;
    private readonly byte level;

    private readonly ushort money;

    public MonsterInfo(string name, string description, ushort health, byte agility, byte accuracy, byte resistance, 
        byte level, ushort money)
    {
        this.name = name;
        this.description = description;
        this.health = health;
        this.agility = agility;
        this.accuracy = accuracy;
        this.resistance = resistance;
        this.level = level;

        this.money = money;
    }

    public ref readonly string GetName() { return ref name; }
    public ref readonly string GetDescription() { return ref description; }
    public ref readonly ushort GetHealth() { return ref health; }
    public ref readonly byte GetAgility() { return ref agility; }
    public ref readonly byte GetAccuracy() { return ref accuracy; }
    public ref readonly byte GetResistance() { return ref resistance; }
    public ref readonly byte GetLevel() { return ref level; }
    public ref readonly ushort GetMoney() { return ref money; }
}
