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
        int cuadradito = 0;
        int tropa = 0;
        ImageSource tropaicon;
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
                    //Ejercitovisible.Add(new UnitDisplay(character));
                }

            this.InitializeComponent();
            
             Cuadricula = new CuadriculaMapa(32, 18, null,this);
            Map.Children.Add(Cuadricula);
            Cuadricula.Name = "Cuadricula";
            Cuadricula.SetValue(Grid.RowSpanProperty, 3);
            Cuadricula.SetValue(Grid.ColumnSpanProperty, 3);
            Cuadricula.CreateTileImages();

            //  MyGrid Cuadricula = new MyGrid();
            //Map.Children.Add(Cuadricula);
            //Cuadricula.Name = "Cuadricula";
            //Cuadricula.SetValue(Grid.RowSpanProperty,1);
            //Cuadricula.SetValue(Grid.ColumnSpanProperty, 1);

            //Cuadricula.Columns = 32;
            //Cuadricula.Rows = 18;

            //Cuadricula.CreateTileImages();
        }

        private void cuadriculagrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        public void cuadriculagrid_Drop(object sender, DragEventArgs e)
        {
            // GridViewItem g =tropasgrid.Items[0] as GridViewItem;
            //tropasgrid.Items.RemoveAt(0);
            // MyGrid g = Map.Children[0] as MyGrid;//la cuadricula entera

            //Grid g2 = g.Children[cuadradito] as Grid;
            // ImageSource i = Ejercito[0].GetImage() ;
            Unit myi = tropasgrid.SelectedItem as Unit;
            if (myi != null)
            {
                int id = ur.id;
                ImageSource i = tropaicon;

                // Image im=   new Image();
                // im.Source = i;

                UserControl cc = sender as UserControl;

                UnitDisplay ui = new UnitDisplay(Army.army[id]);

                ui.unit.colum = Grid.GetColumn(cc);
                ui.unit.row = Grid.GetRow(cc);
                cc.Content = ui;


                //tropasgrid.SelectedItem el que has clickado se guarda bien aqui
                //tropasgrid.Items.Remove(tropasgrid.SelectedItem);
                int limite = myi.sitiolista;
                Ejercito.RemoveAt(myi.sitiolista);
                foreach (Unit character in Ejercito)
                {
                    if (character.sitiolista > limite)
                    {
                        character.sitiolista--;

                    }
                }
            }
         //  tropasgrid.Items.Remove(tropasgrid.SelectedItem);
            // cc.Source = i;

            // Grid back = cc.Content as Grid;
            //back.Children.Add(im);
            //  g2.Children.Add(im);
            // cuadradito += 1;

            //g.Background = 


        }

        private void statbattlebutton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));

        }

        private void tropasgrid_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            Unit u = e.Items[0] as Unit;
            gitem = sender as GridView;
          //  GridViewItem g = e.Items[0] as GridViewItem;
            
           // gitem = g;
          
            /*  GridView g = sender as GridView;
              GridViewHeaderItem gh = g.Items[0] as GridViewHeaderItem;
              Unit u = gh.Content as Unit;*/
            //UnitDisplay ud = sender as UnitDisplay;
            //Unit u =  as Unit;
            // UnitDisplay item = e.Items[0] as UnitDisplay;
            //Unit u = item.unit;
            ur = u;
           // tropaicon = u.GetImage();

        }
       public  void colocaTropa()
        { 
        
        }
    }
}
