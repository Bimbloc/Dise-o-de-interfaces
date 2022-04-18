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

        public int value { get; }
        public string stat { get; }

        public Unit equipedChar;

        public string GetWeight()
        {
            if (weight <= 0)
                return "-";
            else return weight.ToString();


        }
    };

    public class Item : Object
    {
        public Item(int value_, string stat_, string effectDescription_) : base(value_, stat_)
        {
        }

        void onUse() 
        {
            
        }
    };

    public class Equipment : Object
    {
        public Equipment(int value_, string stat_, string effectDescription_) : base(value_, stat_)
        {
        }
        void onEquip() { }

        void onDeEquip() { }
    };

    public class Unit : ObservableObject
    {
        public int[] stats { get; set; }

        public string name { get; set; }
        public string icon { get; set; }

        public BitmapImage GetImage()
        {
            return new BitmapImage(new Uri("ms-appx:///Assets/Units/" + icon));
        }
    }

    public class Inventory
    {
        public static List<Object> Objetos = new List<Object>()
        {
            new Object()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Byleth(M)")

             },
            new Object()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Byleth(F)")

             },
            new Object()
            {
                name = "Silver Sword",
                effectDescription = "Streght +25",
                weight = 15,
                equipedChar = Army.GetUnitByName("Corrin(F)")

             },
            new Object()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Corrin(F)")

             },
            new Object()
            {
                name = "Small Potion",
                effectDescription = "Restores 10 Hp",
                weight = 0,
                equipedChar = Army.GetUnitByName("Corrin(F)")

             },
            new Object()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Pyra")

             },
            new Object()
            {
                name = "Long Bow",
                effectDescription = "Def -10",
                weight = 10,
                equipedChar = Army.GetUnitByName("Pyra")
             },
            new Object()
            {
                name = "Potion",
                effectDescription = "Restores 20 Hp",
                weight = 0,
                equipedChar = Army.GetUnitByName("Pyra")

             },
            new Object()
            {
                name = "Silver Sword",
                effectDescription = "Streght +25",
                weight = 15,
                equipedChar = Army.GetUnitByName("Chrom")

             },
          };


        public static IList<Object> GetAllObjects()
        {
            return Objetos;
        }
    }


    public class Army
    {
        public static List<Unit> army = new List<Unit>()
        {
            new Unit()
            {
                name = "Byleth(F)",
                icon = "byleth_female.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Byleth(M)",
                icon = "byleth_male.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Chrom",
                icon = "chrom.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Corrin(F)",
                icon = "corrin_female.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Corrin(M)",
                icon = "corrin_male.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Ike",
                icon = "ike.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Lucina",
                icon = "lucina.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Marth",
                icon = "marth.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Pyra",
                icon = "pyra.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Richter",
                icon = "richter.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Robin",
                icon = "robin.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Roy",
                icon = "roy.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
            new Unit()
            {
                name = "Zelda",
                icon = "zelda.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 }
             },
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

