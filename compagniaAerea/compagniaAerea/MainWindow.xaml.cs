using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace compagniaAerea
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void dipendente_click(object sender, System.EventArgs e)
        {
            sinistra.Visibility = Visibility.Hidden;
            destra.Visibility = Visibility.Hidden;
            gridDipendente.Visibility = Visibility.Visible;

        }

        private void cliente_click(object sender, RoutedEventArgs e)
        {
            sinistra.Visibility = Visibility.Hidden;
            destra.Visibility = Visibility.Hidden;
            cliente.Visibility = Visibility.Visible;
        }
    }
}
