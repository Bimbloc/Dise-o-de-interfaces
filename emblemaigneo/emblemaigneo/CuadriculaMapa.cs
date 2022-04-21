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
    class CuadriculaMapa : MyGrid
    {
        public Grid[,] tiles { get; set; }
        public UserControl[,] contentControls { get; set; }

        MainPage mainPage;

        public CuadriculaMapa(int columnas, int filas, MainPage mainPage_) 
        {
            tiles = new Grid[columnas, filas];
            contentControls = new UserControl[columnas, filas];

            Columns = columnas;
            Rows = filas;
            CreateTileImages();

            mainPage = mainPage_;
        }

        public void CreateTileImages()
        {
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    //crea la tile, que tiene el color de la casilla
                    Grid tile = new Grid();
                    tile.SetValue(Grid.ColumnProperty, i);
                    tile.SetValue(Grid.RowProperty, j);
                    tile.Background = new SolidColorBrush(Color.FromArgb(100, 50, 50, 50));

                    tiles[i, j] = tile;

                    //crea el contentControl que controla las unidades de esa casilla
                    UserControl contentControl = new UserControl();
                    contentControl.SetValue(Grid.ColumnProperty, i);
                    contentControl.SetValue(Grid.RowProperty, j);
                    contentControl.SetValue(Control.IsTabStopProperty, true);
                    contentControl.SetValue(Control.UseSystemFocusVisualsProperty, true);
                    contentControl.XYFocusKeyboardNavigation = XYFocusKeyboardNavigationMode.Enabled;

                    contentControl.PointerReleased += new PointerEventHandler(ControlHost_PointerReleased);
                    contentControl.PointerPressed += new PointerEventHandler(onTileClick);
                    contentControl.GotFocus += new RoutedEventHandler(onControlFocus);

                    contentControl.Content = tile;
                    contentControls[i, j] = contentControl;

                    Children.Add(contentControl);
                }
            }
        }

        private void onTileClick(object sender, RoutedEventArgs e)
        {
            UserControl tileCC = sender as UserControl;

            tileCC.Focus(FocusState.Keyboard);

            //si hay unidades en la casilla abre el menu de acciones 
            if (tileCC.Content is UnitDisplay)
            {
                UnitDisplay unitDisp = tileCC.Content as UnitDisplay;

                mainPage.ShowActionMenu();
            }

            //tile.Background = new SolidColorBrush(Color.FromArgb(150, 50, 200, 50)); 
        }

        private void onControlFocus(object sender, RoutedEventArgs e)
        {
            UserControl tileCC = sender as UserControl;

            tileCC.Focus(FocusState.Keyboard);

            //si hay unidades en la casilla abre el menu de acciones 
            if (tileCC.Content is UnitDisplay)
            {
                UnitDisplay unitDisp = tileCC.Content as UnitDisplay;

                mainPage.ShowInfoBox();
                mainPage.SetSelectedUnit(unitDisp.unit);
            }

            else 
            {
                mainPage.CollapseInfoBox();
            }
        }

        private void ControlHost_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
        }
    }
}
