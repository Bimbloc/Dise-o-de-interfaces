using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace emblemaigneo
{
    public enum Stats { STR, MAG, DEX, SPD, LCK, DEF, RES, CHA };

    public class Object
    {
        public Object(int value_, string stat_)
        {
            value = value_;
            effectDescription = stat_;
        }

        public Object() { }

        public Object(Object obj)
        {
            name = obj.name;
            weight = obj.weight;
            effectDescription = obj.effectDescription;
        }

        public string name { get; set; }
        public int weight { get; set; }
        public string icon { get; set; }
        public string effectDescription { get; set; }

        public bool isUsable { get; set; }

        public int value { get; }
        public string stat { get; }

        public Unit equipedChar;

        public int range { get; set; }

        public virtual bool OnUseEquip(Unit u) { return true; }
        public virtual void OnDeEquip(Unit u) { }


        public string GetWeight()
        {
            if (weight <= 0)
                return "-";
            else return weight.ToString();
        }

        public BitmapImage GetImage()
        {
            if (equipedChar == null)
                return new BitmapImage(new Uri("ms-appx:///Assets/LockScreenLogo.scale-200.png"));
            return equipedChar.GetImage();
        }
    };

    public class Item : Object
    {
        public Item(int value_, string stat_, string effectDescription_) : base(value_, stat_)
        {
        }

        public Item()
        {
            isUsable = true;
        }

        public override bool OnUseEquip(Unit u)
        {
            u.hp += 20;

            return true;
        }

    };

    public class Equipment : Object
    {
        public Equipment(int value_, string stat_, string effectDescription_) : base(value_, stat_)
        {
        }

        public Equipment()
        {
            isUsable = false;
        }

        public override bool OnUseEquip(Unit u)
        {
            u.stats[0] += 20;

            return true;
        }

        public override void OnDeEquip(Unit u)
        {
            u.stats[0] -= 20;
        }
    };

    public class Unit : ObservableObject
    {
        public Unit(int maxHp_)
        {
            maxHp = maxHp_;
            hp = maxHp - 3;

            Random rnd = new Random();
            exp = rnd.Next(0, 100);
        }

        public int[] stats { get; set; }

        public string name { get; set; }
        public string icon { get; set; }

        public int maxHp { get; }
        public int hp { get; set; }

        public double exp { get; set; }

        public int row { get; set; } = -1;
        public int colum { get; set; } = -1;
        public int id { get; set; } = -1;
        public int sitioLista { get; set; } = -1;

        public BitmapImage GetImage()
        {
            return new BitmapImage(new Uri("ms-appx:///Assets/Units/" + icon));
        }

        public string getHpDisplay()
        {
            return hp + "/" + maxHp;
        }
        public double getHpPercentage()
        {
            float currHP = hp;
            float maxHP = maxHp;

            return Math.Round(currHP / maxHp * 100);
        }
    }

    public class Inventory
    {
        public static List<Object> Objetos = new List<Object>()
        {
            new Equipment()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Byleth(M)"),
                range = 1

             },
            new Equipment()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Byleth(F)"),
                range = 1

             },
            new Equipment()
            {
                name = "Silver Sword",
                effectDescription = "Streght +25",
                weight = 15,
                equipedChar = Army.GetUnitByName("Corrin(F)"),
                range = 1

             },
            new Equipment()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Corrin(F)"),
                range = 1

             },
            new Item()
            {
                name = "Small Potion",
                effectDescription = "Restores 10 Hp",
                weight = 0,
                equipedChar = Army.GetUnitByName("Corrin(F)")

             },
            new Equipment()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Pyra")

             },
            new Equipment()
            {
                name = "Long Bow",
                effectDescription = "Def -10",
                weight = 10,
                equipedChar = Army.GetUnitByName("Pyra"),
                range = 2
             },
            new Item()
            {
                name = "Potion",
                effectDescription = "Restores 20 Hp",
                weight = 0,
                equipedChar = Army.GetUnitByName("Pyra")

             },
            new Equipment()
            {
                name = "Silver Sword",
                effectDescription = "Streght +25",
                weight = 15,
                equipedChar = Army.GetUnitByName("Chrom"),
                range = 1

             },
          };


        public static IList<Object> GetAllObjects()
        {
            return Objetos;
        }

        public static List<Object> getItemsByUnit(Unit unit) 
        {
            selectedUnit = unit;

            List<Object> items = Objetos.FindAll(isEquiped);

            return items;
        }

        private static bool isEquiped(Object item) 
        {
            if (item.equipedChar != null) return selectedUnit.name == item.equipedChar.name;

            return false;
        }

        static Unit selectedUnit;
    }


    public class Army
    {
        public static List<Unit> army = new List<Unit>()
        {
            new Unit(30)
            {
                name = "Byleth(F)",
                icon = "byleth_female.png",

                stats = new int[8] { 10, 23, 20, 25, 10, 15, 20, 25 },
                id =0,
                sitioLista=0
             },
            new Unit(30)
            {
                name = "Byleth(M)",
                icon = "byleth_male.png",

                stats = new int[8] { 17, 15, 20, 25, 10, 15, 20, 25 },
                id =1,
                sitioLista=1
             },
            new Unit(26)
            {
                name = "Chrom",
                icon = "chrom.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id =2,
                sitioLista=2
             },
            new Unit(25)
            {
                name = "Corrin(F)",
                icon = "corrin_female.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=3,
                sitioLista=3
             },
            new Unit(25)
            {
                name = "Corrin(M)",
                icon = "corrin_male.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=4,
                sitioLista=4
             },
            new Unit(17)
            {
                name = "Ike",
                icon = "ike.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=5,
                sitioLista=5
             },
            new Unit(27)
            {
                name = "Lucina",
                icon = "lucina.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=6,
                sitioLista=6
             },
            new Unit(20)
            {
                name = "Marth",
                icon = "marth.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=7,
                sitioLista=7
             },
            new Unit(19)
            {
                name = "Pyra",
                icon = "pyra.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=8,
                sitioLista=8
             },
            new Unit(18)
            {
                name = "Richter",
                icon = "richter.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=9,
                sitioLista=9
             },
            new Unit(21)
            {
                name = "Robin",
                icon = "robin.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=10,
                sitioLista=10
             },
            new Unit(22)
            {
                name = "Roy",
                icon = "roy.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=11,
                sitioLista=11
             },
            new Unit(13)
            {
                name = "Zelda",
                icon = "zelda.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=12,
                sitioLista=12
             },
            new Unit(-1)
            {
                name = "placeholder",

                icon = "transparente.png"
            }
          };


        public static IList<Unit> GetAllUnits()
        {
            return army;
        }

        public static Unit GetUnitByName(string name)
        {
            foreach (Unit character in army)
            {
                if (character.name == name)
                    return character;
            }

            return army[0];
        }
    }
}

