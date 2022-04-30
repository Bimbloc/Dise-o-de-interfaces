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

        private Unit selectedUnit_;
        public Unit selectedUnit 
        { 
            get => selectedUnit_; 
            set {
                Set(ref selectedUnit_, value);
                setStats();
            }
        }

        public enum State { MOVING, ATTACKING, ACTION_MENU, MAP_NAVIGATING };

        public State state { get; set; } = State.MAP_NAVIGATING;

        public MapLogic() 
        {
            selectedUnit = Army.army[0];
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
