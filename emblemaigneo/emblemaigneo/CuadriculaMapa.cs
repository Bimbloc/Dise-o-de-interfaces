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
        InicioBatalla inicioBatalla;
        public struct PointerPos
        {
            public int x;
            public int y;
        }

        PointerPos pointer;

        public CuadriculaMapa(int columnas, int filas, MainPage mainPage_ = null,InicioBatalla inibatalla = null) 
        {
            tiles = new Grid[columnas, filas];
            contentControls = new UserControl[columnas, filas];

            pointer.x = 0;
            pointer.y = 0;
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            Columns = columnas;
            Rows = filas;

            mainPage = mainPage_;
            inicioBatalla = inibatalla;
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

            if (e.Key == Windows.System.VirtualKey.GamepadA || e.Key == Windows.System.VirtualKey.Space || e.Key == Windows.System.VirtualKey.Enter)
                e.Handled = true;

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
                        contentControl.SetValue(Control.AllowDropProperty,true);
                        contentControl.Drop += new DragEventHandler(dropeaTropa);
                        contentControl.KeyDown += new KeyEventHandler(InitBatKeyDown);
                    }
                    contentControls[i, j] = contentControl;

                    Children.Add(contentControl);

                    createTileGrid(i, j);
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

                    contentControls[unit.colum, unit.row].Content = unitDisplay;
                }
            }
        }

        private void onTileKeyDown(object sender, KeyRoutedEventArgs e) 
        {
            if (e.Key == Windows.System.VirtualKey.GamepadA || e.Key == Windows.System.VirtualKey.Space || e.Key == Windows.System.VirtualKey.Enter)
            {
                UserControl tileCC = sender as UserControl;

                if (mainPage.getState() == MapLogic.State.MAP_NAVIGATING)
                {
                    //si hay unidades en la casilla abre el menu de acciones 
                    if (tileCC.Content is UnitDisplay)
                    {
                        mainPage.ShowActionMenu();

                        mainPage.setState(MapLogic.State.ACTION_MENU);
                    }
                }

                else if (mainPage.getState() == MapLogic.State.MOVING) 
                {
                    mainPage.setState(MapLogic.State.MAP_NAVIGATING);

                    UnitDisplay unit = contentControls[mainPage.Logic.selectedUnit.colum, mainPage.Logic.selectedUnit.row].Content as UnitDisplay;

                    createTileGrid(mainPage.Logic.selectedUnit.colum, mainPage.Logic.selectedUnit.row);

                    tileCC.Content = unit;

                    mainPage.Logic.selectedUnit.colum = GetColumn(tileCC);
                    mainPage.Logic.selectedUnit.row = GetRow(tileCC);

                    clearBackground();
                }

                e.Handled = true;
            }
        }

        private void onTileClick(object sender, RoutedEventArgs e)
        {
            UserControl tileCC = sender as UserControl;

            if (mainPage.getState() == MapLogic.State.MAP_NAVIGATING)
            {
                //si hay unidades en la casilla abre el menu de acciones 
                if (tileCC.Content is UnitDisplay)
                {
                    UnitDisplay unitDisp = tileCC.Content as UnitDisplay;

                    mainPage.ShowInfoBox();
                    mainPage.SetSelectedUnit(unitDisp.unit);

                    mainPage.ShowActionMenu();
                }

                else tileCC.Focus(FocusState.Keyboard);
            }

            else if (mainPage.getState() == MapLogic.State.MOVING)
            {
                if (isInRange(4, GetColumn(tileCC), GetRow(tileCC)))
                {
                    UnitDisplay unit = contentControls[mainPage.Logic.selectedUnit.colum, mainPage.Logic.selectedUnit.row].Content as UnitDisplay;

                    createTileGrid(mainPage.Logic.selectedUnit.colum, mainPage.Logic.selectedUnit.row);

                    tileCC.Content = unit;

                    mainPage.Logic.selectedUnit.colum = GetColumn(tileCC);
                    mainPage.Logic.selectedUnit.row = GetRow(tileCC);
                }

                mainPage.setState(MapLogic.State.MAP_NAVIGATING);

                clearBackground();
            }

            //actualiza la posicion de pointer
            pointer.y = GetRow(tileCC);
            pointer.x = GetColumn(tileCC);
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
        private async void dropeaTropa(object sender, DragEventArgs e)
        {
            int id = int.Parse(await e.DataView.GetTextAsync());

            inicioBatalla.cuadriculagrid_Drop(sender, e, id);
        }
        void InitBatKeyDown(object sender, KeyRoutedEventArgs e)
        {
            inicioBatalla.MiGrid_KeyDown(sender, e);
        }
        public void drawCircularRange(int range, int x, int y, bool attack) 
        {
            bool[,] marcas = new bool[Columns, Rows];

            Color color;

            if (attack) color = Color.FromArgb(100, 255, 50, 50);
            else color = Color.FromArgb(100, 50, 50, 255);

            drawCircleRec(range, 0, x, y, marcas, color);
        }

        void drawCircleRec(int maxRange, int actRange, int x, int y, bool[,] marcas, Color color)
        {
            if (x > -1 && y > -1 && x < Columns && y < Rows) 
            {
                Grid tile = contentControls[x, y].Content as Grid;

                if (!marcas[x, y])
                {
                    if (tile != null)
                    {
                        tile.Background = new SolidColorBrush(color);
                        marcas[x, y] = true;
                    }
                    if (actRange + 1 < maxRange)
                    {
                        drawCircleRec(maxRange, actRange + 1, x + 1, y, marcas, color);
                        drawCircleRec(maxRange, actRange + 1, x - 1, y, marcas, color);
                        drawCircleRec(maxRange, actRange + 1, x, y + 1, marcas, color);
                        drawCircleRec(maxRange, actRange + 1, x, y - 1, marcas, color);
                    }
                }
            }
        }

        void createTileGrid(int x, int y) 
        {
            Grid tile = new Grid();
            tile.Background = new SolidColorBrush(Color.FromArgb(100, 50, 50, 50));

            contentControls[x, y].Content = tile;
        }

        void clearBackground() 
        {
            for (int i = 0; i < Columns; i++) 
            {
                for (int j = 0; j < Rows; j++) 
                {
                    Grid tile = contentControls[i, j].Content as Grid;
                    tile.Background = new SolidColorBrush(Color.FromArgb(100, 50, 50, 50));
                }
            }
        }

        bool isInRange(int range, int targetX, int targetY) 
        {
            int x = Math.Abs(targetX - mainPage.Logic.selectedUnit.colum);
            int y = Math.Abs(targetY - mainPage.Logic.selectedUnit.row);

            return x + y <= range;
        }
    }
}
