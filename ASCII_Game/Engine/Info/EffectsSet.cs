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

    public sbyte GetAgility() { return agility; }
    public sbyte GetCharisma() { return charisma; }
    public sbyte GetEndurance() { return endurance; }
    public sbyte GetAccuracy() { return accuracy; }
    public sbyte GetResistance() { return resistance; }
    public sbyte GetLuck() { return luck; }
    public long GetTimeOfAction() { return timeOfAction; }

    public string[] GetInfo()
    {
        return new[] { "Agility: " + agility, "Charisma: " + charisma, "Endurance: " + endurance, 
            "Accuracy: " + accuracy, "Resistance: " + resistance, "Luck: " + luck, 
            "Time of action: " + timeOfAction};
    }
}
