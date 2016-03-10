﻿using System;
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
        int gridPrec = -1;//prima variabile negativa visto che la arraylist parte da 0
        int gridCorrente = -1;//logicamente anche questa la faccio partire da -1 perchè lasciarla vuota mi dava problemi e se è vuota non è null in c#
        Boolean rdbState = true;
        ArrayList gridchange = new ArrayList();//ho creato una array list in modo da dare a ogni grid un numero,un id identificativo in questo caso partendo da 0
        Grid grid;
        String textInBox; //contenitore del text telle dextbox
        //Variabile per la stringa di connessione
        Gestione_Cliente gestione_cliente;
        
        
         public MainWindow()
        {
            //appena apro il main faccio queste 3 cose cioè popolo la lista e dentro a un contenitore Grid ci metto la prima pagina.
            InitializeComponent();
            populateGrid();
            grid = (Grid)gridchange[2];//in questo caso la pagina di prenotazione
            grid.Visibility = Visibility.Visible;
           gestione_cliente = new Gestione_Cliente();// la classe può esssere richiamata anche sotto se si vuole

        }


        //Click del bottone Registrazione presente nella form di Registrazione
        private void Registrazione_Cliente(object sender, RoutedEventArgs e)
        {
          // gestione_cliente = new Gestione_Cliente();  
            //inserimento dati nel metodo
            gestione_cliente.Registrazione_Cliente(Nometxt.Text,Cognometxt.Text,
                (DateTime) DataNascitaPicker.SelectedDate ,
                Usernametxt.Text, Passwordtxt.Password,
                conferma_password.Password , 
                Indirizzotxt.Text, Telefonotxt.Text,
                Emailtxt.Text, StatoCombobox.Name, 
                Regionetxt.Text, Cittàtxt.Text, 
                Int32.Parse(Captxt.Text),
                CodiceFiscaletxt.Text);
            if (gestione_cliente.statoRegistrazioneCliente())
            {
                MessageBox.Show("registrazione avvenuta con successo");
                this.gridCorrente = 2;
                currentGrid(this.gridCorrente);
                

            }
            
        }

        private void click_apriFormClienteNonRegistrato(object sender, RoutedEventArgs e)
        {
        
         
        }

        private void click_apriRegistrazione(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 1;
            currentGrid(this.gridCorrente);
           


        }

        private void click_apriFormClienteRegistrato(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 4;
            currentGrid(this.gridCorrente);
            MIGestioneAerei.Visibility = Visibility.Visible;
            MIGestioneTariffario.Visibility = Visibility.Visible;
            MIturni.Visibility = Visibility.Visible;
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

            //gridLogIn.Visibility = Visibility.Visible;
            this.gridCorrente = 0;
            currentGrid(this.gridCorrente);

        }

        private void Registrazione_Click(object sender, RoutedEventArgs e)
        {
            //  gridRegistrazione.Visibility = Visibility.Visible;
            this.gridCorrente = 1;
            currentGrid(this.gridCorrente);
          

        }

        public void populateGrid()
        { //popolamento della lista
            gridchange.Add(gridLogIn);//grid di logIn pos 0
            gridchange.Add(gridRegistrazione);//grid di registrazione pos 1
            gridchange.Add(gridSelezionaVolo);//grid di seleziona lavoro pos 2 
            gridchange.Add(grid_ricerca_biglietto);//grid di ricerca biglietto pos 3
            gridchange.Add(gridDipendente);//grid dipendente pos 4
        }

        public void currentGrid(int num)  {
            if (this.gridCorrente != this.gridPrec)
            {/* metodo di apertura e chiusura delle grid,d'ora in avanti per aprire basta 
                                          aggiungere nel popolamento la grid che si vuole aprire
                                          vedere la posizione in cui è 
                                          aggiungerla il numero di posizione al this.gridCurrent e passarlo a questo metodo e possiamo creare 12123213123 ∞ diciamo*/

                grid.Visibility = Visibility.Hidden;//la prima grid che pprende la rende invisibile visto che ha controllato se la pagina è cambiata

                grid = (Grid)gridchange[this.gridCorrente];//carico una nuova grid
                grid.Visibility = Visibility.Visible;
                
               
                this.gridPrec = this.gridCorrente;//metto nella grid precedente il valore della corrente in modo che quando cambia ha già il valore impostato
            }
        }

        private void prenotaVolo_Click(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 2;
            currentGrid(this.gridCorrente);
        }

        private void InfoBiglietto_Click(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 3;
            currentGrid(this.gridCorrente);
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 0;
            currentGrid(this.gridCorrente);
            MIGestioneAerei.Visibility = Visibility.Hidden;
            MIGestioneTariffario.Visibility = Visibility.Hidden;
            MIturni.Visibility = Visibility.Hidden;

        }
        //non essendoci più il metodo place holder ho dovuto costruire una cosa simile sia per le textBox
         private void InFocus(object sender, RoutedEventArgs e) 
        {
           
            TextBox tb = (TextBox)sender;
            this.textInBox = tb.Text;
            tb.Text = "";
        }

        private void OutFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Equals(""))
            { 
                tb.Text = this.textInBox;
            }
        }

        private void InPasswordFocus(object sender, RoutedEventArgs e)//non essendoci più il metodo place holder ho dovuto costruire una cosa simile sia per la password
        {
            PasswordBox pb = (PasswordBox)sender;
            this.textInBox = pb.Password;
            pb.Password = "";
        }

        private void OutPasswordFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            if (pb.Password.Equals(""))
            {
                pb.Password = this.textInBox;
            }
        }

        private void btnCercaVolo_Click(object sender, RoutedEventArgs e)
        {

            gestione_cliente.Cerca_volo();
            
         
        }
    }
      

}
