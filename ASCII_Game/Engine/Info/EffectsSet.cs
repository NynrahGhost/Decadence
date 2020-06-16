public class EffectsSet 
{
    private readonly sbyte agility;
    private readonly sbyte charisma;
    private readonly sbyte endurance;
    private readonly sbyte accuracy;
    private readonly sbyte resistance;
    private readonly sbyte luck;

    private readonly long timeOfAction;

    public EffectsSet(sbyte agility, sbyte charisma, sbyte endurance, sbyte accuracy, 
        sbyte resistance, sbyte luck, long timeOfAction)
    {
        this.agility = agility;
        this.charisma = charisma;
        this.endurance = endurance;
        this.accuracy = accuracy;
        this.resistance = resistance;
        this.luck = luck;
        this.timeOfAction = timeOfAction;
    }

    public ref readonly sbyte GetAgility() { return ref agility; }
    public ref readonly sbyte GetCharisma() { return ref charisma; }
    public ref readonly sbyte GetEndurance() { return ref endurance; }
    public ref readonly sbyte GetAccuracy() { return ref accuracy; }
    public ref readonly sbyte GetResistance() { return ref resistance; }
    public ref readonly sbyte GetLuck() { return ref luck; }
    public ref readonly long GetTimeOfAction() { return ref timeOfAction; }
}
