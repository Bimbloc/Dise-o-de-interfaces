﻿using System;
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

        public override void OnDeEquip(Unit u) {
            u.stats[0] -= 20;
        }
    };

    public class Unit :  ObservableObject
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
        public int sitiolista { get; set; } = -1;

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
                equipedChar = Army.GetUnitByName("Byleth(M)")

             },
            new Equipment()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Byleth(F)")

             },
            new Equipment()
            {
                name = "Silver Sword",
                effectDescription = "Streght +25",
                weight = 15,
                equipedChar = Army.GetUnitByName("Corrin(F)")

             },
            new Equipment()
            {
                name = "Iron Sword",
                effectDescription = "Streght +20",
                weight = 20,
                equipedChar = Army.GetUnitByName("Corrin(F)")

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
                equipedChar = Army.GetUnitByName("Pyra")
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
            new Unit(30)
            {
                name = "Byleth(F)",
                icon = "byleth_female.png",

                stats = new int[8] { 10, 23, 20, 25, 10, 15, 20, 25 },
                id =0,
                sitiolista=0
             },
            new Unit(30)
            {
                name = "Byleth(M)",
                icon = "byleth_male.png",

                stats = new int[8] { 17, 15, 20, 25, 10, 15, 20, 25 },
                id =1,
                sitiolista=1
             },
            new Unit(26)
            {
                name = "Chrom",
                icon = "chrom.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id =2,
                sitiolista=2
             },
            new Unit(25)
            {
                name = "Corrin(F)",
                icon = "corrin_female.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=3,
                sitiolista=3
             },
            new Unit(25)
            {
                name = "Corrin(M)",
                icon = "corrin_male.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=4,
                sitiolista=4
             },
            new Unit(17)
            {
                name = "Ike",
                icon = "ike.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=5,
                sitiolista=5
             },
            new Unit(27)
            {
                name = "Lucina",
                icon = "lucina.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=6,
                sitiolista=6
             },
            new Unit(20)
            {
                name = "Marth",
                icon = "marth.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=7,
                sitiolista=7
             },
            new Unit(19)
            {
                name = "Pyra",
                icon = "pyra.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=8,
                sitiolista=8
             },
            new Unit(18)
            {
                name = "Richter",
                icon = "richter.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=9,
                sitiolista=9
             },
            new Unit(21)
            {
                name = "Robin",
                icon = "robin.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=10,
                sitiolista=10
             },
            new Unit(22)
            {
                name = "Roy",
                icon = "roy.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=11,
                sitiolista=11
             },
            new Unit(13)
            {
                name = "Zelda",
                icon = "zelda.png",

                stats = new int[8] { 10, 15, 20, 25, 10, 15, 20, 25 },
                id=12,
                sitiolista=12
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

