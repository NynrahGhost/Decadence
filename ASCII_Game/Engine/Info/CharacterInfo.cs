class CharacterInfo
{
    //Ім'я нпс
    private readonly string name;
    //Здоров'є
    private readonly ushort health;
    //Можливість уникати атаки
    private readonly byte agility;
    //Вплив на діалоги, репутацію
    private readonly byte charisma;
    //Сила (впливає на вагу предметів, що можна переносити)
    private readonly byte endurance;
    //Точність
    private readonly byte accuracy;
    //зменшує час дії дебафів збільшує час дії бафів
    private readonly byte resistance;
    //Удача героя (випадіння предметів)
    private readonly byte luck;
    //Рівень (чисто формальний показник)
    private readonly byte level;

    //Стала кількість монет (якщо герой торговець, то не має різниці скільки купив, продав товарів)
    private readonly uint money;

    public CharacterInfo(string name, ushort health, byte agility, byte charisma, byte endurance, 
        byte accuracy, byte resistance, byte luck, byte level, uint money)
    {
        this.name = name;
        this.health = health;
        this.agility = agility;
        this.charisma = charisma;
        this.endurance = endurance;
        this.accuracy = accuracy;
        this.resistance = resistance;
        this.luck = luck;
        this.level = level;
        this.money = money;
    }

    public string GetName() { return name; }
    public ushort GetHealth() { return health; }
    public byte GetAgility() { return agility; }
    public byte GetCharisma() { return charisma; }
    public byte GetEndurance() { return endurance; }
    public byte GetAccuracy() { return accuracy; }
    public byte GetResistance() { return resistance; }
    public byte GetLuck() { return luck; }
    public byte GetLevel() { return level; }
    public uint GetMoney() { return money; }
}
