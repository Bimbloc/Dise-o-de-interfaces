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

        public bool gameStarted { get; set; } = false;

        MainPage mainPage;

        public struct PointerPos
        {
            public int x;
            public int y;
        }

        PointerPos pointer;

        public CuadriculaMapa(int columnas, int filas, MainPage mainPage_ = null) 
        {
            tiles = new Grid[columnas, filas];
            contentControls = new UserControl[columnas, filas];

            pointer.x = 0;
            pointer.y = 0;
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            Columns = columnas;
            Rows = filas;

            mainPage = mainPage_;
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            bool changed = false;

            if (e.Key == Windows.System.VirtualKey.Up || e.Key == Windows.System.VirtualKey.GamepadLeftThumbstickUp
                || e.Key == Windows.System.VirtualKey.GamepadDPadUp)
            {
                if (pointer.y > 0) pointer.y--;
                e.Handled = true;

                changed = true;
            }
            else if (e.Key == Windows.System.VirtualKey.Down || e.Key == Windows.System.VirtualKey.GamepadLeftThumbstickDown
                || e.Key == Windows.System.VirtualKey.GamepadDPadDown)
            {
                if (pointer.y < Rows - 1) pointer.y++;
                e.Handled = true;

                changed = true;
            }
            else if (e.Key == Windows.System.VirtualKey.Left || e.Key == Windows.System.VirtualKey.GamepadLeftThumbstickLeft
                || e.Key == Windows.System.VirtualKey.GamepadDPadLeft)
            {
                if (pointer.x > 0) pointer.x--;
                e.Handled = true;

                changed = true;
            }
            else if (e.Key == Windows.System.VirtualKey.Right || e.Key == Windows.System.VirtualKey.GamepadLeftThumbstickRight
                || e.Key == Windows.System.VirtualKey.GamepadDPadRight)
            {
                if (pointer.x < Columns - 1) pointer.x++;
                e.Handled = true;

                changed = true;
            }

            if (changed)
            {
                FocusPointer();
            }
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

                    //eventos en la cuadricula cuando se ha iniciado el juego
                    if (gameStarted)
                    {
                        contentControl.PointerReleased += new PointerEventHandler(ControlHost_PointerReleased);
                        contentControl.PointerPressed += new PointerEventHandler(onTileClick);
                        contentControl.KeyDown += new KeyEventHandler(onTileKeyDown);
                        contentControl.GotFocus += new RoutedEventHandler(onControlFocus);
                    }
                    //eventos menu inicio
                    else 
                    { 
                    
                    }

                    contentControl.Content = tile;
                    contentControls[i, j] = contentControl;

                    Children.Add(contentControl);
                }
            }
        }

        public void placeUnitsInGrid() 
        {
            foreach (Unit unit in Army.army) 
            {
                if (unit.row != -1) 
                {
                    UnitDisplay unitDisplay = new UnitDisplay(unit);

                    contentControls[unit.row, unit.colum].Content = unitDisplay;
                }
            }
        }

        private void onTileKeyDown(object sender, KeyRoutedEventArgs e) 
        {
            if (e.Key == Windows.System.VirtualKey.GamepadA || e.Key == Windows.System.VirtualKey.Space) 
            {
                UserControl tileCC = sender as UserControl;

                //si hay unidades en la casilla abre el menu de acciones 
                if (tileCC.Content is UnitDisplay)
                {
                    UnitDisplay unitDisp = tileCC.Content as UnitDisplay;

                    mainPage.ShowActionMenu();
                }
            }
        }

        private void onTileClick(object sender, RoutedEventArgs e)
        {
            UserControl tileCC = sender as UserControl;

            //si hay unidades en la casilla abre el menu de acciones 
            if (tileCC.Content is UnitDisplay)
            {
                UnitDisplay unitDisp = tileCC.Content as UnitDisplay;

                mainPage.ShowActionMenu();
            }

            else
            {
                //actualiza la posicion de pointer
                pointer.x = GetRow(tileCC);
                pointer.y = GetColumn(tileCC);

                tileCC.Focus(FocusState.Keyboard);
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

        public void FocusPointer()
        {
            contentControls[pointer.x, pointer.y].Focus(FocusState.Keyboard);
        }
    }
}
