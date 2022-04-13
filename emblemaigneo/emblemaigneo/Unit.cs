using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emblemaigneo
{
    public enum Stats { STR, MAG, DEX, SPD, LCK, DEF, RES, CHA };

    public class Object
    {
        public Object(int value_, string stat_) 
        {
            value = value_;
            stat = stat_;
        }

        public string name { get; set; }
        public string icon { get; set; }
        public string effectDescription { get; set; }

        public int value { get; }
        public string stat { get; }
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

    public class Unit
    {
        int[] stats = new int[8];
    }
}
