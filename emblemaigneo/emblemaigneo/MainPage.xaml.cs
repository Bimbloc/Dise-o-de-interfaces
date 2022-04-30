﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace emblemaigneo
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int n = 0;
        CuadriculaMapa Cuadricula;

        public MainPage()
        {
            this.InitializeComponent();

            Cuadricula= new CuadriculaMapa(32, 18, this);
            Map.Children.Add(Cuadricula);
            Cuadricula.Name = "Cuadricula";
            Cuadricula.SetValue(Grid.RowSpanProperty, 3);
            Cuadricula.SetValue(Grid.ColumnSpanProperty, 3);

            Cuadricula.gameStarted = true;
            Cuadricula.CreateTileImages();
            Cuadricula.placeUnitsInGrid();
        }

        public MapLogic Logic { get; } = new MapLogic();

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            n++;
            Logic.selectedUnit = Army.army[n];
        }

        public void ShowActionMenu() 
        {
            ActionMenu.Visibility = Visibility.Visible;
            ActionMenu.Focus(FocusState.Keyboard);
            ActionMenu.IsFocusEngaged = true;
        }

        public void CollapseActionMenu()
        {
            ActionMenu.Visibility = Visibility.Collapsed;
        }

        public void ShowInfoBox() 
        {
            InfoBox.Visibility = Visibility.Visible;
        }

        public void CollapseInfoBox()
        {
            InfoBox.Visibility = Visibility.Collapsed;
        }

        public void SetSelectedUnit(Unit unit_) 
        {
            Logic.selectedUnit = unit_;
        }

        private void ActionMenu_FocusDisengaged(Control sender, FocusDisengagedEventArgs args)
        {
            CollapseActionMenu();
        }
    }
}
