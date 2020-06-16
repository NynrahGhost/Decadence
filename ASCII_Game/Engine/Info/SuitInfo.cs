class SuitInfo : ItemInfo
{
    private readonly float Protection;
    private readonly uint ParametersId;

    public SuitInfo(string name, ushort weight, string description, ushort price, 
        float protection, uint parametersId) :
        base(name, weight, description, price)
    {
        this.Protection = protection;
        this.ParametersId = parametersId;
    }

    public ref readonly float GetProtection() { return ref Protection; }
    public ref readonly uint GetParametersId() { return ref ParametersId; }
}
