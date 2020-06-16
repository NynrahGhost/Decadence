using System.Linq;

public class MedicineInfo : ItemInfo
{
    private readonly uint ParametersId;

    public MedicineInfo(string name, ushort weight, string description, ushort price, uint parametersId) :
        base(name, weight, description, price)
    {
        ParametersId = parametersId;
    }

    public uint GetParametersId() { return ParametersId; }

    public override string[] GetInfo()
    {
        return (new[]{"Name: " + GetName(),
            "Description: " + GetDescription(),
            "Weight: " + GetWeight(),
            "Price: " + GetPrice()}).Concat(GameItemsInfo.Effects[ParametersId].GetInfo()).ToArray();
    }
}
