using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emblemaigneo
{
    public struct SelectedUnitStats
    {
        public int STR { get; set; }
        public int MAG { get; set; }
        public int DEX { get; set; }
        public int SPD { get; set; }
        public int LCK { get; set; }
        public int DEF { get; set; }
        public int RES { get; set; }
        public int CHA { get; set; }
    }

    public class MapLogic : ObservableObject
    {
        private SelectedUnitStats unitStats_;
        public SelectedUnitStats unitStats { get => unitStats; }

        private Unit selectedUnit_;
        public Unit selectedUnit 
        { 
            get => selectedUnit_; 
            set { 
                Set(ref selectedUnit_, value);
                setStats();
            } 
        }

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
        }
    }
}
