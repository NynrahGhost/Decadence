using System.Collections.Generic;

public enum PlayerType : byte
{
    Soldier = 1,//Солдат
    Resident,//Звичайний житель
    Bandit//Бандит
}
public enum ItemType : byte
{
    Simple = 1,
    Quest,
    Medicine,
    Weapon,
    Cartridge,
    Suit
}
public class GameItemsInfo
{
    public static readonly Dictionary<uint, ItemInfo> SimpleItems = new Dictionary<uint, ItemInfo>();
    public static readonly Dictionary<uint, QuestItemInfo> QuestItems = new Dictionary<uint, QuestItemInfo>();
    public static readonly Dictionary<uint, MedicineInfo> MedicineItems = new Dictionary<uint, MedicineInfo>();
    public static readonly Dictionary<uint, WeaponInfo> WeaponItems = new Dictionary<uint, WeaponInfo>();
    public static readonly Dictionary<uint, CartridgeInfo> CartridgeItems = new Dictionary<uint, CartridgeInfo>();
    public static readonly Dictionary<uint, SuitInfo> SuitItems = new Dictionary<uint, SuitInfo>();

    public static readonly Dictionary<uint, EffectsSet> Effects = new Dictionary<uint, EffectsSet>();

    GameItemsInfo()
    {
        //заповнення словників
    }
}
