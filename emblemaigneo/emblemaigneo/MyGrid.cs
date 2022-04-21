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
using Windows.UI.Xaml.Media.Imaging;

namespace emblemaigneo
{
    public class MyGrid : Grid
    {
        public MyGrid()
        {
        }

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(MyGrid), new PropertyMetadata(0, ColumnPropertyChangedCallback));

        public static void ColumnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MyGrid)d).ColumnDefinitions.Clear();
            if (((MyGrid)d).Columns > 0)
            {
                for (int i = 0; i < ((MyGrid)d).Columns; i++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    ((MyGrid)d).ColumnDefinitions.Add(col);
                }
            }
        }

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MyGrid), new PropertyMetadata(0, RowsPropertyChangedCallback));

        public static void RowsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MyGrid)d).RowDefinitions.Clear();
            if (((MyGrid)d).Rows > 0)
            {
                for (int r = 0; r < ((MyGrid)d).Rows; r++)
                {
                    RowDefinition row = new RowDefinition();

                    ((MyGrid)d).RowDefinitions.Add(row);
                }
            }
        }
    }
}
