public class MedicineInfo : ItemInfo
{
    private readonly uint ParametersId;

    public MedicineInfo(string name, ushort weight, string description, ushort price, uint parametersId) :
        base(name, weight, description, price)
    {
        ParametersId = parametersId;
    }

    public ref readonly uint GetParametersId() { return ref ParametersId; }
}
