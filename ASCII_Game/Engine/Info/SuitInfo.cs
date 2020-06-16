using System.Linq;

public class SuitInfo : ItemInfo
{
    private readonly float Protection;
    private readonly uint ParametersId;

    public SuitInfo(string name, ushort weight, string description, ushort price, 
        float protection, uint parametersId) :
        base(name, weight, description, price)
    {
        Protection = protection;
        ParametersId = parametersId;
    }

    public float GetProtection() { return Protection; }
    public uint GetParametersId() { return ParametersId; }
    
    public override string[] GetInfo()
    {
        return (new[]{"Name: " + GetName(),
            "Description: " + GetDescription(),
            "Weight: " + GetWeight(),
            "Price: " + GetPrice(),
            "Protection: " + Protection}).Concat(GameItemsInfo.Effects[ParametersId].GetInfo()).ToArray();
    }
}
