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
        
    private void clkDipendente(object sender, RoutedEventArgs e)
        {
            gridDipendente.Visibility = Visibility.Visible;
            main.Visibility = Visibility.Hidden;
        }

        private void clkret(object sender, RoutedEventArgs e)
        {
            back();//metodo di ritorno al main
        }

        private void clrek(object sender, RoutedEventArgs e)
        {
            back();//metodo di ritorno al main
        }

        private void clkcliente(object sender, RoutedEventArgs e)
        {
            cliente.Visibility = Visibility.Visible;
            main.Visibility = Visibility.Hidden;
        }
        private void back()
        {
            cliente.Visibility = Visibility.Hidden;
            gridDipendente.Visibility = Visibility.Hidden;
            main.Visibility = Visibility.Visible;
        }

    }
     
}
