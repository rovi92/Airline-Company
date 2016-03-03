using System;
using System.Collections;
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
      
        Boolean rdbState = true;


        private void click_apriFormClienteNonRegistrato(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void click_apriRegistrazione(object sender, RoutedEventArgs e)
        {
            gridRegistrazione.Visibility = Visibility.Visible;
            
        }

        private void click_apriFormClienteRegistrato(object sender, RoutedEventArgs e)
        {
            //Qui il codice della form per il cliente registrato
            MessageBox.Show("Apri form registrato");
        }

        private void rdbAndataRitorno_Checked(object sender, RoutedEventArgs e)
        {
            if (this.rdbState == false)
            {
                dataRitorno.Visibility = Visibility.Visible;
                tblRitorno.Visibility = Visibility.Visible;
                this.rdbState = true;
            }
        }

        private void rdbSoloAndata_Checked(object sender, RoutedEventArgs e)
        {
            this.rdbState = false;
            dataRitorno.Visibility = Visibility.Hidden;
            tblRitorno.Visibility = Visibility.Hidden;
        }

        private void Accedi_Click(object sender, RoutedEventArgs e)
        {
            gridSelezionaVolo.Visibility = Visibility.Hidden;
            gridLogIn.Visibility = Visibility.Visible;
        }

        private void Registrazione_Click(object sender, RoutedEventArgs e)
        {
            gridRegistrazione.Visibility = Visibility.Visible;
            
        }
    }

}
