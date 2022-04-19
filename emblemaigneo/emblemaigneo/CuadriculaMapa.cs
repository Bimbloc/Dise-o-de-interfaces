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
        public ContentControl[,] contentControls { get; set; }

        MainPage mainPage;

        public CuadriculaMapa(int columnas, int filas, MainPage mainPage_) 
        {
            tiles = new Grid[columnas, filas];
            contentControls = new ContentControl[columnas, filas];

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

                    Children.Add(tile);
                    tiles[i, j] = tile;

                    //crea el contentControl que controla las unidades de esa casilla
                    ContentControl contentControl = new ContentControl();
                    contentControl.SetValue(Grid.ColumnProperty, i);
                    contentControl.SetValue(Grid.RowProperty, j);
                    contentControl.SetValue(Control.IsTabStopProperty, true);
                    contentControl.SetValue(Control.UseSystemFocusVisualsProperty, true);

                    contentControl.Tapped += new TappedEventHandler(onTileClick);
                    contentControls[i, j] = contentControl;

                    Children.Add(contentControl);
                }
            }
        }

        private void onTileClick(object sender, RoutedEventArgs e)
        {
            ContentControl tileCC = sender as ContentControl;

            //si hay unidades en la casilla abre el menu de acciones 
            if (tileCC.Content != null)
            {
                mainPage.ShowActionMenu();
            }

            //tile.Background = new SolidColorBrush(Color.FromArgb(150, 50, 200, 50)); 
        }
    }
}
