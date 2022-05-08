using System;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace emblemaigneo
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        public Menu()
        {
            this.InitializeComponent();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            
            
                // Cuando no se restaura la pila de navegación, navegar a la primera página,
                // configurando la nueva página pasándole la información requerida como
                //parámetro de navegación
                Frame.Navigate(typeof(Opciones));
           
            // Asegurarse de que la ventana actual está activa.
            Window.Current.Activate();
        }

        private void Newgame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EligeMapa));

        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InicioBatalla));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

            App.Current.Exit();
        }
    }
}
