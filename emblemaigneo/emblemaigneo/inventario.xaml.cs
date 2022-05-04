using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace emblemaigneo
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    /// 

    public sealed partial class inventario : Page
    {
        Unit selectedUnit;

        DispatcherTimer timer;

        public ObservableCollection<Object> Inventario { get; } = new ObservableCollection<Object>();

        public inventario()
        {
            if (Inventario != null)
                foreach (Object obj in Inventory.GetAllObjects())
                {
                    Inventario.Add(obj);
                }




            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // If e.Parameter is a string, set the TextBlock's text with it.
            if (e?.Parameter is string unitName)
            {
                selectedUnit = Army.GetUnitByName(unitName);
            }

            base.OnNavigatedTo(e);
        }

        private void ImageGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Remove_Button(object sender, RoutedEventArgs e)
        {
            if (ImageGridView.SelectedItem != null)
            {
                Object o = (Object) ImageGridView.SelectedItem;

                if (o.equipedChar != null)
                {
                    o.equipedChar = null;
                    ChangeItem(o);
                }             
            }        
        }

        private void EquipUse_Button(object sender, RoutedEventArgs e)
        {
            if (ImageGridView.SelectedItem != null)
            {
                Object o = (Object)ImageGridView.SelectedItem;

                Unit lastUnit = o.equipedChar;

                if (lastUnit == null)
                {
                    o.equipedChar = selectedUnit;
                    o.OnUseEquip(selectedUnit);
                    ChangeItem(o);

                }
                else if (lastUnit != selectedUnit)
                {
                    o.OnDeEquip(lastUnit);
                    o.equipedChar = selectedUnit;
                    if (!o.isUsable)
                        o.OnUseEquip(selectedUnit);

                    ChangeItem(o);
                }
                else if (lastUnit == selectedUnit)
                {
                    if (o.isUsable)
                    {
                        o.OnUseEquip(selectedUnit);
                        Inventario.Remove(o);
                    }
                }
            }                     
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void ChangeItem(Object o)
        {
            int index = Inventario.IndexOf(o);
            Inventario.Remove(o);
            Inventario.Insert(index, o);
            if (index == 0)
                ImageGridView.SelectedIndex = index + 1;
            else
                ImageGridView.SelectedIndex = index - 1;
            ImageGridView.SelectedIndex = index;
        }

        private void ImageGridView_KeyDown(object sender, KeyRoutedEventArgs e)
        {

            switch (e.Key)
            {
                case VirtualKey.GamepadY:
                    EquipUse_Button(sender, null);
                    break;
                case VirtualKey.GamepadX:
                    Remove_Button(sender, null);
                    break;
                default:
                    break;
            }
        }
    }
}
