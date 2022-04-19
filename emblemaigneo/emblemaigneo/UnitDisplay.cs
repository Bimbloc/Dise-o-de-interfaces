using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace emblemaigneo
{
    public class UnitDisplay : Grid
    {
        public Unit unit { get; set; }

        Image icon;

        public UnitDisplay(Unit unit_) 
        {
            unit = unit_;

            icon = new Image();
            icon.Source = unit.GetImage();

            Children.Add(icon);
        }
    }
}
