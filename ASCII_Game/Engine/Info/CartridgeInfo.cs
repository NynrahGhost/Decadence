using System;
using System.Collections.Generic;
using System.Text;


public class CartridgeInfo : ItemInfo
{
    public CartridgeInfo(string name, ushort weight, string description, ushort price) :
        base(name, weight, description, price)
    {
    }
    public override string[] GetInfo()
    {
        return new[]{"Name: " + GetName(),
            "Description: " + GetDescription(),
            "Weight: " + GetWeight(),
            "Price: " + GetPrice()};
    }
}
