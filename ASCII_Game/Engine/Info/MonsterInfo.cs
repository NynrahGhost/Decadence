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

    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public ushort GetHealth() { return health; }
    public byte GetAgility() { return agility; }
    public byte GetAccuracy() { return accuracy; }
    public byte GetResistance() { return resistance; }
    public byte GetLevel() { return level; }
    public ushort GetMoney() { return money; }
}
