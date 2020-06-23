using System;
using System.Collections.Generic;
using System.IO;

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

    //можливі бафи/дебафи
    public static readonly Dictionary<uint, EffectsSet> Effects = new Dictionary<uint, EffectsSet>();

    //випадіння предметів за вбивство монстра, key - id монстра
    //public static readonly Dictionary<ushort, ItemGenerator> MonsterItems = new Dictionary<ushort, ItemGenerator>();//todo
    //випадіння предметів за відкриття "скрині зі скарбами", key - id локації
    //public static readonly Dictionary<ushort, ItemGenerator> LocationItems = new Dictionary<ushort, ItemGenerator>();//todo

    private string path;

    public GameItemsInfo(string path)
    {
        this.path = path;
        FillSimpleItems();
        FillQuestItems();
        FillWeaponItems();
        FillMedicineItems();
        FillCartridgeItems();
        FillSuitItems();
        FillEffectsInfo();
    }

    private void FillSimpleItems()
    {
        string[] read;
        char[] seperators = { ';' };

        StreamReader sr = new StreamReader(path + "/SimpleItemsInfo.csv");

        string data = sr.ReadLine();

        uint itemId;
        string name;
        ushort weight;
        string description;
        ushort price;

        while ((data = sr.ReadLine()) != null)
        {
            read = data.Split(seperators, StringSplitOptions.None);
            itemId = uint.Parse(read[0]);
            name = read[1];
            weight = ushort.Parse(read[2]);
            description = read[3];
            price = ushort.Parse(read[4]);
            SimpleItems.Add(itemId, new ItemInfo(name, weight, description, price));
        }
    }
    private void FillQuestItems()
    {
        string[] read;
        char[] seperators = { ';' };

        StreamReader sr = new StreamReader(path + "/QuestItemsInfo.csv");

        string data = sr.ReadLine();

        uint itemId;
        string name;
        ushort weight;
        string description;
        ushort price;

        while ((data = sr.ReadLine()) != null)
        {
            read = data.Split(seperators, StringSplitOptions.None);
            itemId = uint.Parse(read[0]);
            name = read[1];
            weight = ushort.Parse(read[2]);
            description = read[3];
            price = ushort.Parse(read[4]);
            QuestItems.Add(itemId, new QuestItemInfo(name, weight, description, price));
        }
    }
    private void FillMedicineItems()
    {
        string[] read;
        char[] seperators = { ';' };

        StreamReader sr = new StreamReader(path + "/MedicineItemsInfo.csv");

        string data = sr.ReadLine();

        uint itemId;
        string name;
        ushort weight;
        string description;
        ushort price;
        uint parametersId;

        while ((data = sr.ReadLine()) != null)
        {
            read = data.Split(seperators, StringSplitOptions.None);
            itemId = uint.Parse(read[0]);
            name = read[1];
            weight = ushort.Parse(read[2]);
            description = read[3];
            price = ushort.Parse(read[4]);
            parametersId = uint.Parse(read[5]);
            MedicineItems.Add(itemId, new MedicineInfo(name, weight, description, price, parametersId));
        }
    }
    private void FillWeaponItems()
    {
        string[] read;
        char[] seperators = { ';' };

        StreamReader sr = new StreamReader(path + "/WeaponItemsInfo.csv");

        string data = sr.ReadLine();

        uint itemId;
        string name;
        ushort weight;
        string description;
        ushort price;
        uint cartridgeId;
        uint damage;
        float cooldown;
        uint effectId;
        float effectProbability;

        while ((data = sr.ReadLine()) != null)
        {
            read = data.Split(seperators, StringSplitOptions.None);
            itemId = uint.Parse(read[0]);
            name = read[1];
            weight = ushort.Parse(read[2]);
            description = read[3];
            price = ushort.Parse(read[4]);
            cartridgeId = uint.Parse(read[5]);
            damage = uint.Parse(read[6]);
            cooldown = float.Parse(read[7]);
            effectId = uint.Parse(read[8]);
            effectProbability = float.Parse(read[9]);
            WeaponItems.Add(itemId, new WeaponInfo(name, weight, description, price, cartridgeId, damage, 
                cooldown, effectId, effectProbability));
        }
    }

    private void FillCartridgeItems()
    {
        string[] read;
        char[] seperators = { ';' };

        StreamReader sr = new StreamReader(path + "/CartridgeItemsInfo.csv");

        string data = sr.ReadLine();

        uint itemId;
        string name;
        ushort weight;
        string description;
        ushort price;

        while ((data = sr.ReadLine()) != null)
        {
            read = data.Split(seperators, StringSplitOptions.None);
            itemId = uint.Parse(read[0]);
            name = read[1];
            weight = ushort.Parse(read[2]);
            description = read[3];
            price = ushort.Parse(read[4]);
            CartridgeItems.Add(itemId, new CartridgeInfo(name, weight, description, price));
        }
    }
    private void FillSuitItems()
    {
        string[] read;
        char[] seperators = { ';' };

        StreamReader sr = new StreamReader(path + "/SuitItemsInfo.csv");

        string data = sr.ReadLine();

        uint itemId;
        string name;
        ushort weight;
        string description;
        ushort price;
        float protection;
        uint parametersId;

        while ((data = sr.ReadLine()) != null)
        {
            read = data.Split(seperators, StringSplitOptions.None);
            itemId = uint.Parse(read[0]);
            name = read[1];
            weight = ushort.Parse(read[2]);
            description = read[3];
            price = ushort.Parse(read[4]);
            protection = float.Parse(read[5]);
            parametersId = uint.Parse(read[6]);
            SuitItems.Add(itemId, new SuitInfo(name, weight, description, price, protection, parametersId));
        }
    }
    private void FillEffectsInfo()
    {
        string[] read;
        char[] seperators = { ';' };

        StreamReader sr = new StreamReader(path + "/EffectsInfo.csv");

        string data = sr.ReadLine();

        uint effectId;
        sbyte agility;
        sbyte charisma;
        sbyte endurance;
        sbyte accuracy;
        sbyte resistance;
        sbyte luck;
        float timeOfAction;

        while ((data = sr.ReadLine()) != null)
        {
            read = data.Split(seperators, StringSplitOptions.None);
            effectId = uint.Parse(read[0]);
            agility = sbyte.Parse(read[1]);
            charisma = sbyte.Parse(read[2]);
            endurance = sbyte.Parse(read[3]);
            accuracy = sbyte.Parse(read[4]);
            resistance = sbyte.Parse(read[5]);
            luck = sbyte.Parse(read[6]);
            timeOfAction = float.Parse(read[7]);
            Effects.Add(effectId, new EffectsSet(agility,charisma,endurance,accuracy,resistance,luck,timeOfAction));
        }
    }
}
