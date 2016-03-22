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
        InterfacciaError errore = new Error();


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
        #region grid registrazione del cliente
        private void Registrazione_Cliente(object sender, RoutedEventArgs e)
        {
            errore.ValueText(Nometxt);
            errore.ValueText(Cognometxt);
            errore.ValueText(Usernametxt);
            errore.valuePassword(Passwordtxt);
            errore.valuePassword(conferma_password);
            errore.ValueText(Emailtxt);
            errore.ValueText(Indirizzotxt);
            errore.ValueText(Telefonotxt);
            errore.ValueText(Regionetxt);
            errore.ValueText(Cittàtxt);
            errore.ValueText(Captxt);
            errore.ValueText(CodiceFiscaletxt);
            errore.checkPs(Passwordtxt, conferma_password);
            errore.longTxt(CodiceFiscaletxt, 16);
            errore.longTxt(Captxt, 5);
            errore.longTxt(Telefonotxt, 10);
            //inserimento dati nel metodo
            if (errore.checkText())
            {
                gestione_cliente.Registrazione_Cliente(Nometxt.Text, Cognometxt.Text,
                (DateTime)DataNascitaPicker.SelectedDate,
                Usernametxt.Text, Passwordtxt.Password,
                conferma_password.Password,
                Indirizzotxt.Text, Telefonotxt.Text,
                Emailtxt.Text, StatoCombobox.Name,
                Regionetxt.Text, Cittàtxt.Text,
                Int32.Parse(Captxt.Text),
                CodiceFiscaletxt.Text);


            }
            else {
                MessageBox.Show(errore.codError());
            }
        }
        #endregion

        #region grid cerca volo
       
        #region bottone cerca volo
        private void btnCercaVolo_Click(object sender, RoutedEventArgs e)
        {
            // myDatabase = new myDatabaseUniboAirlineDataContext(); //connessione al database

            //dataGrid.ItemsSource = prova;
            errore.ValueText(txtPartenza);
            errore.ValueText(txtDestinazioneVolo);
            if(errore.checkText())
            {
                var cerca_volo = gestione_cliente.Cerca_volo(txtPartenza.Text, txtDestinazioneVolo.Text, (DateTime)dataPartenza.SelectedDate, (DateTime)dataRitorno.SelectedDate);
                dataGrid.ItemsSource = cerca_volo;
                //LABEL_PROVA.Content = cerca_volo;
            } else
            {
                MessageBox.Show(errore.codError());
            }

        }
        #endregion

        #region radioButton andata/andata e ritorno
        private void rdbSoloAndata_Checked(object sender, RoutedEventArgs e)
        {
            this.rdbState = false;
            dataRitorno.Visibility = Visibility.Hidden;
            tblRitorno.Visibility = Visibility.Hidden;
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

        #endregion

        #endregion

        #region gestione delle grid

        private void click_apriFormClienteNonRegistrato(object sender, RoutedEventArgs e)
        {


        }

        private void Accedi_Click(object sender, RoutedEventArgs e)
        {

            //gridLogIn.Visibility = Visibility.Visible;
            this.gridCorrente = 0;
            currentGrid();

        }

        private void click_apriFormClienteRegistrato(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 4;
            currentGrid();
            MIGestioneAerei.Visibility = Visibility.Visible;
            MIGestioneTariffario.Visibility = Visibility.Visible;
            MIturni.Visibility = Visibility.Visible;
        }

        private void Registrazione_Click(object sender, RoutedEventArgs e)
        {
            //  gridRegistrazione.Visibility = Visibility.Visible;
            this.gridCorrente = 1;
            currentGrid();


        }

        public void populateGrid()
        { //popolamento della lista
            gridchange.Add(gridLogIn);//grid di logIn pos 0
            gridchange.Add(gridRegistrazione);//grid di registrazione pos 1
            gridchange.Add(gridSelezionaVolo);//grid di seleziona lavoro pos 2 
            gridchange.Add(grid_ricerca_biglietto);//grid di ricerca biglietto pos 3
            gridchange.Add(gridDipendente);//grid dipendente pos 4
        }

        public void currentGrid()
        {
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
            currentGrid();
        }

        private void InfoBiglietto_Click(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 3;
            currentGrid();
        }

        private void click_apriRegistrazione(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 1;
            currentGrid();
        }
        
        #endregion

        #region place holder manuale
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 0;
            currentGrid();
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
        #endregion

       

    }
}


    //prova

