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
        int gridPrec = -1;//prima variabile negativa visto che la arraylist parte da 0
        int gridCorrente = -1;//logicamente anche questa la faccio partire da -1 perchè lasciarla vuota mi dava problemi e se è vuota non è null in c#
        Boolean rdbState = true;
        ArrayList gridchange = new ArrayList();//ho creato una array list in modo da dare a ogni grid un numero,un id identificativo in questo caso partendo da 0
        Grid grid;
        String textInBox; //contenitore del text telle dextbox
        //Variabile per la stringa di connessione
        Gestione_utente gestione_cliente;
        VoloImpl volo;
        InterfacciaError errore = new Error();
        Ticket ticket = new TicketImpl();

        public MainWindow()
        {

            gestione_cliente = new Gestione_utente();// la classe può esssere richiamata anche sotto se si vuole
            volo = new VoloImpl();//classe volo
            //appena apro il main faccio queste 3 cose cioè popolo la lista e dentro a un contenitore Grid ci metto la prima pagina.
            InitializeComponent();
            populateGrid();
            grid = (Grid)gridchange[2];//in questo caso la pagina di prenotazione
            grid.Visibility = Visibility.Visible;
            volo.executeTratta();//aggiornamento database locale


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
            errore.longTxt(CodiceFiscaletxt, 16);
            errore.shortTxt(CodiceFiscaletxt, 16);
            errore.longTxt(Captxt, 5);
            errore.longTxt(Telefonotxt, 10);
            errore.checkPs(Passwordtxt, conferma_password);
            //inserimento dati nel metodo
            if (errore.checkText())
            {
                // aggiunta dei metodi di controllo di esistenza dei dati 
                if (gestione_cliente.controlCF(CodiceFiscaletxt.Text).Equals(true) && gestione_cliente.controlloUsername(Usernametxt.Text).Equals(true) && gestione_cliente.controlloEmail(Emailtxt.Text).Equals(true))
                {
                    ComboBoxItem typeItem = (ComboBoxItem)StatoCombobox.SelectedItem;
                    string stato = typeItem.Content.ToString();
                    gestione_cliente.Registrazione_Cliente(Nometxt.Text, Cognometxt.Text,
                   (DateTime)DataNascitaPicker.SelectedDate,
                   Usernametxt.Text, Passwordtxt.Password,
                   conferma_password.Password,
                   Indirizzotxt.Text, Telefonotxt.Text,
                   Emailtxt.Text, stato,
                   Regionetxt.Text, Cittàtxt.Text,
                   Int32.Parse(Captxt.Text),
                   CodiceFiscaletxt.Text);

                    errore.TraverseVisualTree(gridRegistrazione);
                    MessageBox.Show("registrazione avvenuta con successo");
                    this.gridCorrente = 2;
                    volo.executeTratta();
                    currentGrid();
                }

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

            switch (rdbAndataRitorno.IsChecked)
            {
                case true:

                    if (volo.getExistDestination(txtPartenza.Text).Equals(true) &&
                          volo.getExistArrive(txtDestinazioneVolo.Text).Equals(true) &&
                          volo.getExistTimeDestination(dataPartenza.SelectedDate.Value.ToString("yyyy-MM-dd"), "dataPartenza").Equals(true) &&
                          volo.getExistTimeDestination(dataRitorno.SelectedDate.Value.ToString("yyyy-MM-dd"), "dataRitorno").Equals(true))
                    {
                        dataGridRitorno.ItemsSource = volo.getFly(txtDestinazioneVolo.Text, txtPartenza.Text, dataRitorno.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        dataGridAndata.ItemsSource = volo.getFly(txtPartenza.Text, txtDestinazioneVolo.Text, dataPartenza.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    }
                    break;
                case false:

                    //  volo.getExistTimeDestination(dataPartenza.SelectedDate.Value.ToString("yyyy-MM-dd"), "dataPartenza");
                    if (volo.getExistDestination(txtPartenza.Text).Equals(true) &&
                            volo.getExistArrive(txtDestinazioneVolo.Text).Equals(true) &&
                            volo.getExistTimeDestination(dataPartenza.SelectedDate.Value.ToString("yyyy-MM-dd"), "dataPartenza").Equals(true))
                    {
                        dataGridAndata.ItemsSource = volo.getFly(txtPartenza.Text, txtDestinazioneVolo.Text, dataPartenza.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    }
                    break;
            }
        }
        #endregion

        #region bottone prenota
        private void prenota_click(object sender, RoutedEventArgs e)
        {
            //informazioni della grid prenotazione volo salvate in tiket


            //switch grid

            this.gridCorrente = 7;
            currentGrid();

        }
        #endregion

        #region radioButton andata/andata e ritorno
        private void rdbSoloAndata_Checked(object sender, RoutedEventArgs e)
        {
            this.rdbState = false;
            dataGridAndata.Height = 300;
            dataGridRitorno.Visibility = Visibility.Hidden;
            dataRitorno.Visibility = Visibility.Hidden;
            tblRitorno.Visibility = Visibility.Hidden;

        }

        private void rdbAndataRitorno_Checked(object sender, RoutedEventArgs e)
        {
            if (this.rdbState == false)
            {
                dataGridAndata.Height = 150;
                dataGridRitorno.Visibility = Visibility.Visible;
                dataRitorno.Visibility = Visibility.Visible;
                tblRitorno.Visibility = Visibility.Visible;
                this.rdbState = true;
            }
        }

        #endregion

        #region radiobutton classi di volo
        private void rdbEconomy_Checked(object sender, RoutedEventArgs e)
        {
            volo.setClass(rdbEconomy);

        }
        private void rdbBuisness_Checked(object sender, RoutedEventArgs e)
        {
            volo.setClass(rdbBuisness);
        }
        private void rdbFirst_Checked(object sender, RoutedEventArgs e)
        {
            volo.setClass(rdbFirst);
        }
        #endregion
        #endregion
        
        #region cerca biglietto

        private void cercaBiglietto_Click(object sender, RoutedEventArgs e)
        {
            //controllo campi non vuoti
            errore.ValueText(CodiceBigliettotxt);
            errore.ValueText(NomeBigliettotxt);
            errore.ValueText(CognomeBigliettotxt);
            bool conversion_flag;//flag per il controllo della conversione dal cosice biglietto in intero
            if (errore.checkText())
            {
                try//prova conversione e definizione del flag
                {
                    int cosice = Convert.ToInt32(CodiceBigliettotxt.Text);
                    conversion_flag = true;
                }
                catch (InvalidCastException ie)
                {
                    MessageBox.Show(ie.Source, "Conversion Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    conversion_flag = false;
                }
                if (conversion_flag == true)
                {
                    btnConferma_ordine.Visibility = Visibility.Hidden;//nascosto bottone del conferma ordine
                    int codice = Convert.ToInt32(CodiceBigliettotxt.Text);
                    //cambio grid
                    this.gridCorrente = 8;
                    //modifica parametri nella grid di visualizzazione biglietto
                    nomelbl.Content = ticket.getNome(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text);
                    cognomelbl.Content = ticket.getCognome(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text);
                    CFlbl.Content = ticket.getCF(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text);
                    codiceVololbl.Content = ticket.getCodiceVolo(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text);
                    aereoporteAndatalbl.Content = ticket.getAereoportoAndata(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text);
                    aereoportoArrivolbl.Content = ticket.getAereoportoArrivo(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text);
                    oraPartenzalbl.Content = ticket.getOraPartenza(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text).ToString();
                    oraArrivolbl.Content = ticket.getOraArrivo(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text).ToString();
                    dataPartenzalbl.Content = ticket.getDataPartenza(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text).ToString();
                    dataArrivolbl.Content = ticket.getDataArrivo(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text).ToString();
                    totalelbl.Content = ticket.getSpesaTotale(codice, NomeBigliettotxt.Text, CognomeBigliettotxt.Text).ToString();
                }
            }
            else
            {
                MessageBox.Show(errore.codError(), "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        //bottone grid di visualizazione del bigliettoper torna indieto        
        private void btnIndietro_Click(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 3;
            //pulitura delle label
            nomelbl.Content = "";
            cognomelbl.Content = "";
            CFlbl.Content = "";
            codiceVololbl.Content = "";
            aereoporteAndatalbl.Content = "";
            aereoportoArrivolbl.Content = "";
            oraPartenzalbl.Content = "";
            oraArrivolbl.Content = "";
            dataArrivolbl.Content = "";
            dataPartenzalbl.Content = "";
            totalelbl.Content = "";
        }
        #endregion

        #region gestione delle grid

        private void click_apriFormClienteNonRegistrato(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 1;
            gestione_cliente.InitUtente();
            currentGrid();

        }

        private void Accedi_Click(object sender, RoutedEventArgs e)
        {

            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 0;
            currentGrid();

        }



        private void Registrazione_Click(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 1;
            gestione_cliente.InitUtente();
            currentGrid();


        }

        public void populateGrid()
        { //popolamento della lista
            gridchange.Add(gridLogIn);//grid di logIn pos 0
            gridchange.Add(gridRegistrazione);//grid di registrazione pos 1
            gridchange.Add(gridSelezionaVolo);//grid di seleziona volo pos 2 
            gridchange.Add(grid_ricerca_biglietto);//grid di ricerca biglietto pos 3
            gridchange.Add(GridProfiloDipendente);//grid delle informazioni del dipendente posizione 4
            gridchange.Add(GridDipendenteVoli);//grid dei voli dei dipendenti posizione 5
            gridchange.Add(GridDipendentetariffario);//grid del tariffario posizione 6
            gridchange.Add(gridInfoVolo);//grid del tasto prenota posizione 7
            gridchange.Add(viewTicket);//grid del biglietto posizione 8
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
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 2;
            volo.executeTratta();//aggiornamento database locale
            currentGrid();
        }

        private void InfoBiglietto_Click(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 3;
            currentGrid();
        }

        private void click_apriRegistrazione(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 1;
            gestione_cliente.InitUtente();
            currentGrid();
        }
        #region profilo dipendente
        string telefono_dipendente, email_dipendente, indirizzodi_pendente;
        private void cambia_telefonocb_Checked(object sender, RoutedEventArgs e)
        {
            salva_dipendentebtn.Visibility = Visibility.Visible;
            telefono_dipendente = telefonoDipendentetxt.Text;
            telefonoDipendentetxt.IsEnabled = true;
        }

        private void cambia_emailcb_Checked(object sender, RoutedEventArgs e)
        {
            salva_dipendentebtn.Visibility = Visibility.Visible;
            email_dipendente = email_dipendentetxt.Text;
            email_dipendentetxt.IsEnabled = true;

        }
        private void cambia_indirizzocb_Checked(object sender, RoutedEventArgs e)
        {
            salva_dipendentebtn.Visibility = Visibility.Visible;
            indirizzodi_pendente = indirizzo_dipendentetxt.Text;
            indirizzo_dipendentetxt.IsEnabled = true;
        }
        private void salva_dipendentebtn_Click(object sender, RoutedEventArgs e)
        {
            errore.ValueText(telefonoDipendentetxt);
            errore.ValueText(email_dipendentetxt);
            errore.ValueText(indirizzo_dipendentetxt);

            if (errore.checkText())
            {
                cambia_telefono_dipendentecb.IsChecked = false;
                cambia_email_dipendentecb.IsChecked = false;
                cambia_indirizzo_dipendentecb.IsChecked = false;
                //cambio cambio dei dati nel db
            }
            else {
                MessageBox.Show(errore.codError(), "ERRORE!", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        #endregion

        #endregion

        #region place holder manuale

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

        #region grid logIn
        private void click_login(object sender, RoutedEventArgs e)
        {
            errore.ValueText(Login_usernametxt);
            errore.valuePassword(Login_passwordtxt);
            if (errore.checkText())//controllo caratteri non vuoti nelle box
            {

                if (Login_usernametxt.Text.Equals("admin") && Login_passwordtxt.Password.Equals("admin"))
                {

                    MIGestioneVoli.Visibility = Visibility.Visible;
                    MIGestioneTariffario.Visibility = Visibility.Visible;
                    MIProfilo.Visibility = Visibility.Visible;
                    btnLogOut.Visibility = Visibility.Visible;
                    this.gridCorrente = 4;
                    currentGrid();
                }
            }
        }
        #endregion

        #region grid infoVolo dopo il prenota
        private void btnConfermaDati_Click(object sender, RoutedEventArgs e)
        {
            errore.ValueText(nomepasseggerotxt);
            errore.ValueText(cognomepasseggerotxt);
            errore.ValueText(emailpasseggerotxt);
            errore.ValueText(viapasseggerotxt);
            errore.ValueText(cappasseggerotxt);
            errore.ValueText(cfpasseggerotxt);
            if (!errore.checkText())
            {
                MessageBox.Show("ci sono campi vuoti");
            }
        }
        #endregion

        #region dipendente
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 0;
            currentGrid();
            MIGestioneVoli.Visibility = Visibility.Hidden;
            MIGestioneTariffario.Visibility = Visibility.Hidden;
            MIProfilo.Visibility = Visibility.Hidden;
            btnLogOut.Visibility = Visibility.Hidden;



        }

        //tutto ciò che fa parte di dipendente metti qui
        #region btnMenu
        private void click_Tariffario(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 6;
            currentGrid();
        }
        private void Click_gestioneVoli(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 5;
            currentGrid();
        }
        private void Click_profilo(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 4;
            currentGrid();
        }
        #endregion

        #region tariffario

        private void ckbOfferta_Checked(object sender, RoutedEventArgs e)
        {
            borderGridTO.Visibility = Visibility.Visible;
        }

        private void ckbOfferta_Unchecked(object sender, RoutedEventArgs e)
        {
            borderGridTO.Visibility = Visibility.Hidden;
        }

        private void click_salvaPrezzo(object sender, RoutedEventArgs e)
        {
            errore.ValueText(txtCostoTratta);
            if (errore.checkText())
            {

            }
            else {
                MessageBox.Show(errore.codError());
            }
        }

        private void txtPartenza_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void click_applicaOfferta(object sender, RoutedEventArgs e)
        {
            errore.ValueText(txtPsconto);
            if (errore.checkText())
            {

            }
            else {
                MessageBox.Show(errore.codError());
            }
        }

        #endregion

        #region gestione voli

        private void aggiungi_voloClick(object sender, RoutedEventArgs e)
        {
            errore.ValueText(aereoporto_partenzatxt);
            errore.ValueText(aereoporto_arrivotxt);
            errore.ValueText(gatetxt);
            errore.ValueText(orario_partenzatxt);
            errore.ValueText(orario_arrivotxt);

            if (errore.checkText())
            {

            }
            else
            {
                MessageBox.Show(errore.codError());
            }
        }


        #endregion

        #endregion


    }
}
