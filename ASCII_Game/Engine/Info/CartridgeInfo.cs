using System;
using System.Collections.Generic;
using System.Text;


class CartridgeInfo : ItemInfo
{
    private readonly uint WeaponId;

    public CartridgeInfo(string name, ushort weight, string description, ushort price, uint weaponId) :
        base(name, weight, description, price)
    {
        this.WeaponId = weaponId;
    }

    public ref readonly uint GetWeaponId() { return ref WeaponId; }
}
