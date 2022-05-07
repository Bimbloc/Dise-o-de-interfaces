using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class InicioBatalla : Page
    {
        Unit ur;
        CuadriculaMapa Cuadricula;
        GridView gitem;

        public ObservableCollection<Unit> Ejercito { get; } = new ObservableCollection<Unit>();

        public InicioBatalla()
        {

            if (Ejercito != null)
                foreach (Unit character in Army.GetAllUnits())
                {
                    Ejercito.Add(character);
                }

            this.InitializeComponent();

            Cuadricula = new CuadriculaMapa(32, 18, null, this);
            Map.Children.Add(Cuadricula);
            Cuadricula.Name = "Cuadricula";
            Cuadricula.SetValue(Grid.RowSpanProperty, 3);
            Cuadricula.SetValue(Grid.ColumnSpanProperty, 3);
            Cuadricula.CreateTileImages();
        }

        private void cuadriculagrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        public void cuadriculagrid_Drop(object sender, DragEventArgs e, int id)
        {
            Unit unit = Army.army[id];

            UserControl cc = sender as UserControl;

            UnitDisplay ui = new UnitDisplay(unit);

            ui.unit.colum = Grid.GetColumn(cc);
            ui.unit.row = Grid.GetRow(cc);
            cc.Content = ui;


            //tropasgrid.SelectedItem el que has clickado se guarda bien aqui
            //tropasgrid.Items.Remove(tropasgrid.SelectedItem);
            int limite = unit.sitioLista;
            Ejercito.RemoveAt(unit.sitioLista);
            foreach (Unit character in Ejercito)
            {
                if (character.sitioLista > limite)
                {
                    character.sitioLista--;
                }
            }
        }

        private void statbattlebutton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void tropasgrid_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            Unit u = e.Items[0] as Unit;
            gitem = sender as GridView;

            ur = u;

            string id_ = u.id.ToString();

            e.Data.SetText(id_);
        }
        public void colocaTropa()
        {

        }

        private void Reorganizabuton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InicioBatalla));
        }

        public void MiGrid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Enter:
                case Windows.System.VirtualKey.Space:
                case Windows.System.VirtualKey.GamepadY:

                    UserControl cc = sender as UserControl;

                    UnitDisplay ui = new UnitDisplay(Ejercito[0]);

                    ui.unit.colum = Grid.GetColumn(cc);
                    ui.unit.row = Grid.GetRow(cc);
                    cc.Content = ui;
                    Ejercito.RemoveAt(0);

                    foreach (Unit character in Ejercito)
                    {
                        if (character.sitioLista > 0)
                        {
                            character.sitioLista--;
                        }
                    }

                    e.Handled = true;
                    break;

                case Windows.System.VirtualKey.Escape:

                    MiGridCC.Focus(FocusState.Keyboard);
                    MiGridCC.IsFocusEngaged = true;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Menu));
        }
    }
}
