using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emblemaigneo
{
    public struct UnitStats
    {
        public int STR; public int LCK;
        public int MAG; public int DEF;
        public int DEX; public int RES;
        public int SPD; public int CHA;
    }

    public class MapLogic : ObservableObject
    {
        private UnitStats unitStats_;
        public UnitStats unitStats { get => unitStats_; }

        private List<string> equipedItems;

        private string equipped1_;
        public string equipped1 
        {
            get => equipped1_;
            set
            {
                Set(ref equipped1_, value);
            }
        }
        private string equipped2_;
        public string equipped2
        {
            get => equipped2_;
            set
            {
                Set(ref equipped2_, value);
            }
        }
        private string equipped3_;
        public string equipped3
        {
            get => equipped3_;
            set
            {
                Set(ref equipped3_, value);
            }
        }
        private string equipped4_;
        public string equipped4
        {
            get => equipped4_;
            set
            {
                Set(ref equipped4_, value);
            }
        }


        private Unit selectedUnit_;
        public Unit selectedUnit 
        { 
            get => selectedUnit_; 
            set {
                Set(ref selectedUnit_, value);
                setStats();
                equipedItems = getEquipedItems(value);
            }
        }

        public enum State { MOVING, ATTACKING, ACTION_MENU, MAP_NAVIGATING };

        public State state { get; set; } = State.MAP_NAVIGATING;

        public MapLogic() 
        {
            selectedUnit = Army.army[0];
        }

        List<string> getEquipedItems(Unit unit)
        {
            List<string> names = new List<string>();

            List<Object> items = Inventory.getItemsByUnit(selectedUnit);

            for (int i = 0; i < Math.Min(4, items.Count); i++) 
            {
                names.Add(items[i].name);
            }

            for (int j = 0; j < 4; j++) 
            {
                names.Add("");
            }

            equipped1 = names[0];
            equipped2 = names[1];
            equipped3 = names[2];
            equipped4 = names[3];

            return names;
        }

        void setStats() 
        {
            unitStats_.STR = selectedUnit_.stats[0];
            unitStats_.MAG = selectedUnit_.stats[1];
            unitStats_.DEX = selectedUnit_.stats[2];
            unitStats_.SPD = selectedUnit_.stats[3];
            unitStats_.LCK = selectedUnit_.stats[4];
            unitStats_.DEF = selectedUnit_.stats[5];
            unitStats_.RES = selectedUnit_.stats[6];
            unitStats_.CHA = selectedUnit_.stats[7];

            RaisePropertyChanged(nameof(unitStats));
        }
    }
}
