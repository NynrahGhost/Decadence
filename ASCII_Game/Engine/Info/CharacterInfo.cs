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

    public ref readonly string GetName() { return ref name; }
    public ref readonly ushort GetHealth() { return ref health; }
    public ref readonly byte GetAgility() { return ref agility; }
    public ref readonly byte GetCharisma() { return ref charisma; }
    public ref readonly byte GetEndurance() { return ref endurance; }
    public ref readonly byte GetAccuracy() { return ref accuracy; }
    public ref readonly byte GetResistance() { return ref resistance; }
    public ref readonly byte GetLuck() { return ref luck; }
    public ref readonly byte GetLevel() { return ref level; }
    public ref readonly uint GetMoney() { return ref money; }
}
