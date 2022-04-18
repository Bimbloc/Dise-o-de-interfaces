using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class InicioBatalla : Page
    {
        public ObservableCollection<Unit> Ejercito { get; } = new ObservableCollection<Unit>();

        public InicioBatalla()
        {
            
            if (Ejercito != null)
                foreach (Unit character in Army.GetAllUnits())
                {
                    Ejercito.Add(character);
                }

            this.InitializeComponent();
        }

        private void cuadriculagrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void cuadriculagrid_Drop(object sender, DragEventArgs e)
        {
            GridViewItem g =tropasgrid.Items[0] as GridViewItem;
            //tropasgrid.Items.RemoveAt(0);
            //cuadriculagrid.Items.Add(g);


        }
    }
}
