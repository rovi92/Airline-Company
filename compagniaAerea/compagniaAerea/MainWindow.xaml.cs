using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


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
        FlightImpl volo;
        DipendenteVoloImpl dipendentevolo;
        InterfacciaError errore = new Error();
        Ticket ticket = new TicketImpl();
        Dipendente dipendente = new DipendenteImpl();


        public MainWindow()
        {

            gestione_cliente = new Gestione_utente();// la classe può esssere richiamata anche sotto se si vuole
            volo = new FlightImpl();//classe volo
            dipendentevolo = new DipendenteVoloImpl();
            //appena apro il main faccio queste 3 cose cioè popolo la lista e dentro a un contenitore Grid ci metto la prima pagina.
            InitializeComponent();
            populateGrid();
            dataPartenza.DisplayDateStart = DateTime.Today;
            dataRitorno.DisplayDateStart = DateTime.Today;
            grid = (Grid)gridchange[2];//in questo caso la pagina di prenotazione
            grid.Visibility = Visibility.Visible;
            volo.UpdateFlights();//aggiornamento database locale


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
            //errore.longTxt(CodiceFiscaletxt, 16);
            errore.CfCheck(CodiceFiscaletxt, 16);//Prima c'era //errore.shortTxt(CodiceFiscaletxt, 16);
            errore.longTxt(Captxt, 5);
            errore.longTxt(Telefonotxt, 10);
            errore.checkPs(Passwordtxt, conferma_password);
            //inserimento dati nel metodo
            if (errore.checkText())
            {
                // aggiunta dei metodi di controllo di esistenza dei dati 
                if (gestione_cliente.controlCF(CodiceFiscaletxt.Text).Equals(true) && gestione_cliente.controlloEmail(Emailtxt.Text).Equals(true))
                {
                    ComboBoxItem typeItem = (ComboBoxItem)StatoCombobox.SelectedItem;
                    string stato = typeItem.Content.ToString();
                    gestione_cliente.Registrazione_Cliente(Nometxt.Text, Cognometxt.Text,

                   Indirizzotxt.Text,
                  Emailtxt.Text,
                    Cittàtxt.Text,
                   Int32.Parse(Captxt.Text),
                   CodiceFiscaletxt.Text);

                    errore.TraverseVisualTree(gridRegistrazione);
                    MessageBox.Show("Registrazione avvenuta con successo");
                    this.gridCorrente = 2;
                    volo.UpdateFlights();
                    currentGrid();
                }

            }
            else
            {
                MessageBox.Show(errore.codError());
            }
        }
        #endregion

        #region grid cerca volo 

        #region bottone cerca volo
        private void btnCercaVolo_Click(object sender, RoutedEventArgs e)
        {
            gridTipoVolo.Visibility = Visibility.Visible;
            switch (rdbAndataRitorno.IsChecked)
            {
                case true:

                    if (volo.checkDeparture(txtPartenza.Text).Equals(true) &&
                          volo.checkArrival(txtDestinazioneVolo.Text).Equals(true) &&
                          volo.checkDateFlight(dataPartenza.SelectedDate).Equals(true) &&
                          volo.checkDateFlight(dataRitorno.SelectedDate).Equals(true))
                    {

                        dataGridRitorno.ItemsSource = volo.getCustomFlight(txtDestinazioneVolo.Text, txtPartenza.Text, dataRitorno.SelectedDate);
                        dataGridAndata.ItemsSource = volo.getCustomFlight(txtPartenza.Text, txtDestinazioneVolo.Text, dataPartenza.SelectedDate);
                    }
                    break;
                case false:

                    if (volo.checkDeparture(txtPartenza.Text).Equals(true) &&
                            volo.checkArrival(txtDestinazioneVolo.Text).Equals(true) &&
                            volo.checkDateFlight(dataPartenza.SelectedDate).Equals(true))
                    {
                        dataGridAndata.ItemsSource = volo.getCustomFlight(txtPartenza.Text, txtDestinazioneVolo.Text, dataPartenza.SelectedDate);
                    }
                    break;

            }
        }
        #endregion

        #region bottone prenota
        private void prenota_click(object sender, RoutedEventArgs e)
        {
            if (rdbAndataRitorno.IsChecked.Value)
            {

                if (volo.checkFlightSeats(int.Parse(getCellValue(dataGridAndata, 0)), volo.getFlightClassId(), int.Parse(lblPosti.Content.ToString())) && volo.checkFlightSeats(int.Parse(getCellValue(dataGridRitorno, 0)), volo.getFlightClassId(), int.Parse(lblPosti.Content.ToString())))
                {

                    ticket.createBooking(DateTime.Today.ToString("yyyy-MM-dd"),
                               int.Parse(lblPosti.Content.ToString()),
                               0,
                               ticket.getIdTariffa(int.Parse(getCellValue(dataGridAndata, 0)), volo.getFlightClassId()), "Andata");
                    ticket.createBooking(DateTime.Today.ToString("yyyy-MM-dd"),
                               int.Parse(lblPosti.Content.ToString()),
                               0,
                               ticket.getIdTariffa(int.Parse(getCellValue(dataGridRitorno, 0)), volo.getFlightClassId()), "Ritorno");
                    volo.updateFlightSeats(int.Parse(getCellValue(dataGridAndata, 0)), volo.getFlightClassId(), int.Parse(lblPosti.Content.ToString()));
                    volo.updateFlightSeats(int.Parse(getCellValue(dataGridRitorno, 0)), volo.getFlightClassId(), int.Parse(lblPosti.Content.ToString()));
                    btnIndietro.Visibility = Visibility.Hidden;



                    this.gridCorrente = 7;
                    currentGrid();
                }
                else
                {
                    MessageBox.Show("Posti esauriti dio porco");
                }

            }
            else
            {
                if (volo.checkFlightSeats(int.Parse(getCellValue(dataGridAndata, 0)), volo.getFlightClassId(), int.Parse(lblPosti.Content.ToString())))
                {
                    btnIndietro.Visibility = Visibility.Hidden;
                    ticket.createBooking(DateTime.Today.ToString("yyyy-MM-dd"),
                               int.Parse(lblPosti.Content.ToString()),
                               0,
                               ticket.getIdTariffa(int.Parse(getCellValue(dataGridAndata, 0)), volo.getFlightClassId()), "Andata");
                    volo.updateFlightSeats(int.Parse(getCellValue(dataGridAndata, 0)), volo.getFlightClassId(), int.Parse(lblPosti.Content.ToString()));
                    this.gridCorrente = 7;
                    currentGrid();
                }
                else
                {
                    MessageBox.Show("Posti esauriti dio porco");
                }
            }
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
            volo.setFlightClass(rdbEconomy.Content.ToString(), 1);

        }
        private void rdbBuisness_Checked(object sender, RoutedEventArgs e)
        {
            volo.setFlightClass(rdbBuisness.Content.ToString(), 2);
        }
        private void rdbFirst_Checked(object sender, RoutedEventArgs e)
        {
            volo.setFlightClass(rdbFirst.Content.ToString(), 3);
        }
        #endregion
        #endregion

        #region cerca biglietto

        private void cercaBiglietto_Click(object sender, RoutedEventArgs e)
        {
            //controllo campi non vuoti
            errore.ValueText(CodiceBigliettotxt);

            bool conversion_flag;//flag per il controllo della conversione dal cosice biglietto in intero
            int codice = -1;
            if (errore.checkText())
            {
                try//prova conversione e definizione del flag
                {
                    codice = Convert.ToInt32(CodiceBigliettotxt.Text);
                    conversion_flag = true;
                }
                catch (InvalidCastException ie)
                {
                    MessageBox.Show(ie.Source, "Conversion Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    conversion_flag = false;
                }
                if (conversion_flag == true)
                {
                    ticket.getBiglietto(codice);
                    btnConferma_ordine.Visibility = Visibility.Hidden;//nascosto bottone del conferma ordine                
                    //cambio grid
                    this.gridCorrente = 8;
                    currentGrid();
                    //modifica parametri nella grid di visualizzazione biglietto
                    nomelbl.Content = ticket.getNome();
                    cognomelbl.Content = ticket.getCognome();
                    CFlbl.Content = ticket.getCF();
                    codiceVololbl.Content = ticket.getCodiceVolo();
                    aereoporteAndatalbl.Content = ticket.getAereoportoAndata();
                    aereoportoArrivolbl.Content = ticket.getAereoportoArrivo();
                    oraPartenzalbl.Content = ticket.getOraPartenza();
                    oraArrivolbl.Content = ticket.getOraArrivo();
                    dataPartenzalbl.Content = ticket.getDataPartenza();
                    dataArrivolbl.Content = ticket.getDataArrivo();
                    totalelbl.Content = ticket.getSpesaTotale();
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
            currentGrid();
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
            gridchange.Add(GridPagamento);//grid del pagamento 9
            gridchange.Add(gridOfferte);//gird delle offerte 10
            gridchange.Add(gridDipendenteVolo);// grid dei dipendenti in volo su un tratta 11
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
            errore.TraverseVisualTree(gridSelezionaVolo);
            this.gridCorrente = 2;
            volo.UpdateFlights();
            gridTipoVolo.Visibility = Visibility.Hidden;//aggiornamento database locale
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

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 0;
            currentGrid();
            MIGestioneVoli.Visibility = Visibility.Hidden;
            MIProfilo.Visibility = Visibility.Hidden;
            MIofferte.Visibility = Visibility.Hidden;
            btnLogOut.Visibility = Visibility.Hidden;
        }

        /*ricerca del dipendente nell grid profilo dipendete*/
        private void CercaDipendente_click(object sender, RoutedEventArgs e)
        {
            dipendente.getDipendente(Convert.ToInt32(getCellValue(dataProfiliDipendetente, 2)));
            cognomeDipendentetxt.Text = dipendente.getCognome();
            nomeDipendentetxt.Text = dipendente.getNome();
            codiceDipendentetxt.Text = dipendente.getCodice();
            indirizzo_dipendentetxt.Text = dipendente.getIndirizzo();
            dataAssunzione_dipendentetxt.Text = dipendente.getDataAssunzione();
            data_nascita_dipendentetxt.Text = dipendente.getDataNascita();
            genereDipendentetxt.Text = dipendente.getSesso();
            impiegoDipendentetxt.Text = dipendente.getImpiego();
            telefonoDipendentetxt.Text = dipendente.getTelefono();
            indirizzo_dipendentetxt.Text = dipendente.getIndirizzo();
            email_dipendentetxt.Text = dipendente.getEmail();
        }

        string telefono_dipendente, email_dipendente, indirizzodi_pendente;
       
        private void cambia_telefono_dipendentecb_Click(object sender, RoutedEventArgs e)
        {
            if (cambia_telefono_dipendentecb.IsChecked.Value == false)
            {
                telefonoDipendentetxt.IsEnabled = false;
            }
            else
            {
                salva_dipendentebtn.Visibility = Visibility.Visible;
                telefono_dipendente = telefonoDipendentetxt.Text;
                telefonoDipendentetxt.IsEnabled = true;
            }
        }

        private void cambia_email_dipendentecb_Click(object sender, RoutedEventArgs e)
        {
            if (cambia_email_dipendentecb.IsChecked.Value == false)
            {
                email_dipendentetxt.IsEnabled = false;
            }
            else
            {
                salva_dipendentebtn.Visibility = Visibility.Visible;
                email_dipendente = email_dipendentetxt.Text;
                email_dipendentetxt.IsEnabled = true;
            }
        }

        private void cambia_indirizzo_dipendentecb_Click(object sender, RoutedEventArgs e)
        {
            if (cambia_indirizzo_dipendentecb.IsChecked.Value == false)
            {
                indirizzo_dipendentetxt.IsEnabled = false;
            }
            else
            {
                salva_dipendentebtn.Visibility = Visibility.Visible;
                indirizzodi_pendente = indirizzo_dipendentetxt.Text;
                indirizzo_dipendentetxt.IsEnabled = true;
            }
        }

        private void aggiungiDipendente_Click(object sender, RoutedEventArgs e)
        {

            if (aggiungiDipendente.IsChecked.Value == true)
            {
                pulisciDipendente();
                abilitaModificaDipendente();
            }
            else if (aggiungiDipendente.IsChecked.Value == false)
            {
                pulisciDipendente();
                abilitaVisioneDipendente();
            }
        }
        /*3 metodi per pulire e rendenre visibili degli elementi nella grid di profiloDipendente*/
        public void pulisciDipendente()
        {
            cognomeDipendentetxt.Clear();
            nomeDipendentetxt.Clear();
            codiceDipendentetxt.Clear();
            data_nascita_dipendentetxt.Clear();
            telefonoDipendentetxt.Clear();
            dataAssunzione_dipendentetxt.Clear();
            email_dipendentetxt.Clear();
            indirizzo_dipendentetxt.Clear();
            cambia_telefono_dipendentecb.IsChecked = false;
            cambia_email_dipendentecb.IsChecked = false;
            cambia_indirizzo_dipendentecb.IsChecked = false;
            genereDipendentetxt.Clear();
            impiegoDipendentetxt.Clear();
            dipendenteMaschiordb.IsChecked = false;
            dipendenteFemminardb.IsChecked = false;
            dipendentePilotardb.IsChecked = false;
            dipendenteHostessrdb.IsChecked = false;
        }

        public void abilitaModificaDipendente()
        {
            cognomeDipendentetxt.IsEnabled = true;
            nomeDipendentetxt.IsEnabled = true;
            codiceDipendentetxt.Visibility = Visibility.Hidden;
            codice_dipendentetbl.Visibility = Visibility.Hidden;
            nuova_data_nascita_dipendete.Visibility = Visibility.Visible;
            genereDipendentetxt.IsEnabled = true;
            impiegoDipendentetxt.IsEnabled = true;
            indirizzo_dipendentetxt.IsEnabled = true;
            telefonoDipendentetxt.IsEnabled = true;
            email_dipendentetxt.IsEnabled = true;
            cercaDipendentebtn.IsEnabled = false;
            genereDipendentetxt.Visibility = Visibility.Hidden;
            impiegoDipendentetxt.Visibility = Visibility.Hidden;
            dipendenteFemminardb.Visibility = Visibility.Visible;
            dipendenteMaschiordb.Visibility = Visibility.Visible;
            dipendentePilotardb.Visibility = Visibility.Visible;
            dipendenteHostessrdb.Visibility = Visibility.Visible;
        }

        public void abilitaVisioneDipendente()
        {
            cognomeDipendentetxt.IsEnabled = false;
            nomeDipendentetxt.IsEnabled = false;
            codiceDipendentetxt.Visibility = Visibility.Visible;
            codice_dipendentetbl.Visibility = Visibility.Visible;
            nuova_data_nascita_dipendete.Visibility = Visibility.Hidden;
            genereDipendentetxt.IsEnabled = false;
            impiegoDipendentetxt.IsEnabled = false;
            indirizzo_dipendentetxt.IsEnabled = false;
            telefonoDipendentetxt.IsEnabled = false;
            email_dipendentetxt.IsEnabled = false;
            cercaDipendentebtn.IsEnabled = true;
            aggiungiDipendente.IsChecked = false;
            genereDipendentetxt.Visibility = Visibility.Visible;
            impiegoDipendentetxt.Visibility = Visibility.Visible;
            dipendenteFemminardb.Visibility = Visibility.Hidden;
            dipendenteMaschiordb.Visibility = Visibility.Hidden;
            dipendentePilotardb.Visibility = Visibility.Hidden;
            dipendenteHostessrdb.Visibility = Visibility.Hidden;
        }       

        private void salva_dipendentebtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            /*cambio di un solo parametro fra telefono indirizzo email*/
            if (cambia_email_dipendentecb.IsChecked == true || cambia_indirizzo_dipendentecb.IsChecked == true || cambia_telefono_dipendentecb.IsChecked == true)
            {

                errore.ValueText(telefonoDipendentetxt);
                errore.checkEmail(email_dipendentetxt);
                errore.ValueText(indirizzo_dipendentetxt);
                if (errore.checkText())
                {
                    /*inserimento dei dati nel db*/
                    if (cambia_email_dipendentecb.IsChecked == true)
                        dipendente.setEmail(email_dipendentetxt.Text, Convert.ToInt32(codiceDipendentetxt.Text));
                    if (cambia_indirizzo_dipendentecb.IsChecked == true)
                        dipendente.setIndirizzo(indirizzo_dipendentetxt.Text, Convert.ToInt32(codiceDipendentetxt.Text));
                    if (cambia_telefono_dipendentecb.IsChecked == true)
                        dipendente.setTelefono(telefonoDipendentetxt.Text, Convert.ToInt32(codiceDipendentetxt.Text));
                    //reset checkbox
                    cambia_telefono_dipendentecb.IsChecked = false;
                    cambia_email_dipendentecb.IsChecked = false;
                    cambia_indirizzo_dipendentecb.IsChecked = false;
                }
                else
                {
                    MessageBox.Show(errore.codError(), "ERRORE!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (aggiungiDipendente.IsChecked.Value == true)
            {
                cambia_telefono_dipendentecb.IsEnabled = false;
                cambia_email_dipendentecb.IsEnabled = false;
                cambia_indirizzo_dipendentecb.IsEnabled = false;
                {
                    /*controllo campi vuoti*/
                    errore.ValueText(cognomeDipendentetxt);
                    errore.ValueText(nomeDipendentetxt);
                    errore.checkEmail(email_dipendentetxt);
                    errore.ValueText(indirizzo_dipendentetxt);
                    errore.ValueText(telefonoDipendentetxt);
                    if (errore.checkText())
                    {
                        if (nuova_data_nascita_dipendete.Text == "")
                        {
                            MessageBox.Show("Selezionare una data di nascita", "ERRORE!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if ((dipendenteMaschiordb.IsChecked.Value == true || dipendenteFemminardb.IsChecked.Value == true) && (dipendentePilotardb.IsChecked.Value == true || dipendenteHostessrdb.IsChecked.Value == true))
                            {
                                dipendente.createDipendente(nomeDipendentetxt.Text,
                                    cognomeDipendentetxt.Text,
                                    indirizzo_dipendentetxt.Text,
                                    nuova_data_nascita_dipendete.SelectedDate.Value,
                                    currentDate,
                                    email_dipendentetxt.Text,
                                    telefonoDipendentetxt.Text,
                                    dipendenteMaschiordb.IsChecked.Value ? "M" : "F",
                                    dipendentePilotardb.IsChecked.Value,
                                    dipendenteHostessrdb.IsChecked.Value);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(errore.codError(), "ERRORE!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            pulisciDipendente();
            abilitaVisioneDipendente();
            dataProfiliDipendetente.ItemsSource = dipendente.getStaff(); //rimepimento datagrid            
        }

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
        private void login_click(object sender, RoutedEventArgs e)
        {
            errore.ValueText(Login_usernametxt);
            errore.valuePassword(Login_passwordtxt);

            if (errore.checkText())//controllo caratteri non vuoti nelle box
            {

                if (Login_usernametxt.Text.Equals("admin") && Login_passwordtxt.Password.Equals("admin"))
                {
                    MIGestioneVoli.Visibility = Visibility.Visible;
                    MIProfilo.Visibility = Visibility.Visible;
                    MIofferte.Visibility = Visibility.Visible;
                    MIGestioneDipendenteVoli.Visibility = Visibility.Visible;
                    btnLogOut.Visibility = Visibility.Visible;
                    this.gridCorrente = 4;
                    currentGrid();
                    dataProfiliDipendetente.ItemsSource = dipendente.getStaff(); //rimepimento datagrid
                }
            }
            else
            {
                MessageBox.Show("Riempire tutti i campi", "ERRORE!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region grid infoVolo dopo il prenota



        private void btnConfermaDati_Click(object sender, RoutedEventArgs e)
        {
            errore.ValueText(nomepasseggerotxt);
            errore.ValueText(cognomepasseggerotxt);
            errore.checkEmail(emailpasseggerotxt);
            errore.ValueText(viapasseggerotxt);
            errore.CAPCheck(cappasseggerotxt);
            errore.CfCheck(cfpasseggerotxt,16);


            if (errore.checkText())
            {
                this.gridCorrente = 8;
                currentGrid();

                if (btnConferma_ordine.Visibility.Equals(Visibility.Hidden))
                {
                    btnConferma_ordine.Visibility = Visibility.Visible;
                }

                gestione_cliente.Registrazione_Cliente(nomepasseggerotxt.Text,
                    cognomepasseggerotxt.Text,
                    viapasseggerotxt.Text,
                    emailpasseggerotxt.Text,
                    cittàpasseggerotxt.Text,
                    Int32.Parse(cappasseggerotxt.Text),
                    cfpasseggerotxt.Text);

                ticket.insertRecordTicket(gestione_cliente.getLastIdPassenger(cfpasseggerotxt.Text), ticket.getIdPrenotazioneAndata());

                if (cbVenti.IsChecked.Value)
                {
                    ticket.createFlightLuggage(20);
                }
                if (cbTrenta.IsChecked.Value)
                {
                    ticket.createFlightLuggage(30);
                }
                if (cbCinquanta.IsChecked.Value)
                {
                    ticket.createFlightLuggage(50);
                }
                if (ckbpranzo.IsChecked.Value)
                {
                    ticket.createFlightComfort(1);
                }
                if (ckbAcon.IsChecked.Value)
                {
                    ticket.createFlightComfort(2);
                }
                nomelbl.Content = nomepasseggerotxt.Text;
                cognomelbl.Content = cognomepasseggerotxt.Text;
                CFlbl.Content = cfpasseggerotxt.Text;
                codiceVololbl.Content = getCellValue(dataGridAndata, 0);
                aereoporteAndatalbl.Content = volo.getAirportName(getCellValue(dataGridAndata, 1));
                aereoportoArrivolbl.Content = volo.getAirportName(getCellValue(dataGridAndata, 2));
                oraPartenzalbl.Content = getCellValue(dataGridAndata, 6);
                oraArrivolbl.Content = getCellValue(dataGridAndata, 7);
                dataPartenzalbl.Content = ((DateTime)dataPartenza.SelectedDate).ToString("dd-MM-yyyy");
                dataArrivolbl.Content = rdbAndataRitorno.IsChecked.Value ? ((DateTime)dataRitorno.SelectedDate).ToString("dd-MM-yyyy") : ((DateTime)dataPartenza.SelectedDate).ToString("dd-MM-yyyy");             
                totalelbl.Content = ticket.getTicketPrice(ticket.getLastIdBiglietto());
                ticket.setQuantitaPersone(ticket.getQuatitàPersone() - 1);

                ticket.updatePrenotationPrice(ticket.getIdPrenotazioneAndata());
                txtTotale.Text = ticket.getPrenotationPrice(ticket.getIdPrenotazioneAndata()).ToString();
                txtdataPagamento.Text = DateTime.Today.ToString("yyyy-MM-dd");
                gridPagamentoRitorno.Visibility = Visibility.Hidden;
                if (volo.CountFlightLegs(int.Parse(getCellValue(dataGridAndata, 0))) > 1)
                {
                    GridScaloViewTicket.Visibility = Visibility.Visible;
                    lblScalo.Content = volo.getFlightLegDeparture(int.Parse(getCellValue(dataGridAndata, 0)));
                    lblOrarioPartenza.Content = volo.getFlightLegDepartureTime(int.Parse(getCellValue(dataGridAndata, 0)));
                    LblOrarioArrivo.Content = volo.getFlightLegArrivalTime(int.Parse(getCellValue(dataGridAndata, 0)));
                }

            }
            else
            {

                MessageBox.Show(errore.codError());
            }
        }

        private void conferma_ordine_click(object sender, RoutedEventArgs e)
        {
            int biglietti = ticket.getQuatitàPersone();

            GridScaloViewTicket.Visibility = Visibility.Hidden;
            if (rdbAndataRitorno.IsChecked.Value)
            {
                ticket.insertRecordTicket(gestione_cliente.getLastIdPassenger(cfpasseggerotxt.Text), ticket.getIdPrenotazioneRitorno());
                if (cbVenti.IsChecked.Value)
                {
                    ticket.createFlightLuggage(20);
                }
                if (cbTrenta.IsChecked.Value)
                {
                    ticket.createFlightLuggage(30);
                }
                if (cbCinquanta.IsChecked.Value)
                {
                    ticket.createFlightLuggage(50);
                }
                if (ckbpranzo.IsChecked.Value)
                {
                    ticket.createFlightComfort(1);
                }
                if (ckbAcon.IsChecked.Value)
                {
                    ticket.createFlightComfort(2);
                }
                nomelbl.Content = nomepasseggerotxt.Text;
                cognomelbl.Content = cognomepasseggerotxt.Text;
                CFlbl.Content = cfpasseggerotxt.Text;
                codiceVololbl.Content = getCellValue(dataGridRitorno, 0);
                aereoporteAndatalbl.Content = volo.getAirportName(getCellValue(dataGridRitorno, 1));
                aereoportoArrivolbl.Content = volo.getAirportName(getCellValue(dataGridRitorno, 2));
                oraPartenzalbl.Content = getCellValue(dataGridRitorno, 6);
                oraArrivolbl.Content = getCellValue(dataGridRitorno, 7);
                dataPartenzalbl.Content = dataPartenza.SelectedDate.ToString();
                dataArrivolbl.Content = dataRitorno.SelectedDate.ToString();
                totalelbl.Content = ticket.getTicketPrice(ticket.getLastIdBiglietto());


                btnConferma_ordine.Visibility = Visibility.Hidden;
                btnConferma2.Visibility = Visibility.Visible;
                if (volo.CountFlightLegs(int.Parse(getCellValue(dataGridAndata, 0))) > 0)
                {
                    GridScaloViewTicket.Visibility = Visibility.Visible;
                }
                  }
            else
            {
                if (ticket.getQuatitàPersone() > 0)
                {

                    this.gridCorrente = 7;
                    currentGrid();
                    errore.TraverseVisualTree(this.grid);
                }
                else
                {

                    /*   txtTotale.Text = ticket.getPrenotationPrice(ticket.getIdPrenotazioneRitorno()).ToString();
                       txtdataPagamento.Text = DateTime.Today.ToString("yyyy-MM-dd");*/
                    if (gridPagamentoRitorno.IsVisible)
                    {
                        gridPagamentoRitorno.Visibility = Visibility.Hidden;
                    }
                    this.gridCorrente = 9;
                    currentGrid();
                }
                /* txtdataPagamento.Text = DateTime.Today.ToString("yyyy-MM-dd");
              //  txtTotale.Text = totalelbl.Content.ToString(); GridSupplemento.Visibility = Visibility.Hidden;
                btnRegistraCliente.Visibility = Visibility.Visible;
                this.gridCorrente = 9;
                currentGrid();*/
            }


        }

        private void btnConferma2_Click(object sender, RoutedEventArgs e)
        {

            btnConferma_ordine.Visibility = Visibility.Visible;
            btnConferma2.Visibility = Visibility.Hidden;
            if (ticket.getQuatitàPersone() > 0)
            {
                this.gridCorrente = 7;
                currentGrid();
                errore.TraverseVisualTree(this.grid);
            }
            else
            {
                ticket.updatePrenotationPrice(ticket.getIdPrenotazioneAndata());
                txtTotale.Text = ticket.getPrenotationPrice(ticket.getIdPrenotazioneAndata()).ToString();
                ticket.updatePrenotationPrice(ticket.getIdPrenotazioneRitorno());
                txtTotale_Ritorno.Text = ticket.getPrenotationPrice(ticket.getIdPrenotazioneRitorno()).ToString();
                txtdataPagamento.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtdataPagamento_Ritorno.Text = DateTime.Today.ToString("yyyy-MM-dd");
                if (!gridPagamentoRitorno.IsVisible)
                {
                    gridPagamentoRitorno.Visibility = Visibility.Visible;
                }
                this.gridCorrente = 9;
                currentGrid();
            }

        }


        #endregion

        #region GRIDPAGAMENTO
        private void btnConferma3_Click(object sender, RoutedEventArgs e)
        {
            gridTipoVolo.Visibility = Visibility.Hidden;
            ComboBoxItem itemA = (ComboBoxItem)cmbTipoPagamento.SelectedItem;
            string tipoPagamento = itemA.Content.ToString();
           
            ticket.updatePrenotationPrice(ticket.getIdPrenotazioneAndata());
            ticket.insertRecordPagamento(txtdataPagamento.Text, tipoPagamento, ticket.getIdPrenotazioneAndata());
            if (rdbAndataRitorno.IsChecked.Value)
            {
                ComboBoxItem itemR = (ComboBoxItem)cmbTipoPagamento_Ritorno.SelectedItem;
                string tipoPagamentoR = itemR.Content.ToString();
                ticket.insertRecordPagamento(txtdataPagamento.Text, tipoPagamento, ticket.getIdPrenotazioneAndata());
                ticket.updatePrenotationPrice(ticket.getIdPrenotazioneRitorno());
                ticket.insertRecordPagamento(txtdataPagamento_Ritorno.Text, tipoPagamentoR, ticket.getIdPrenotazioneRitorno());
            }

            MessageBox.Show("Grazie per aver scelto la nostra compagnia");
            gridPagamentoRitorno.Visibility = Visibility.Hidden;
            this.gridCorrente = 2;
            currentGrid();
            // ticket.getPopulateDbTicket();
            gridTipoVolo.Visibility = Visibility.Hidden;
            volo.UpdateFlights();
            errore.TraverseVisualTree(gridSelezionaVolo);
        }
        #endregion


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
            pianidivolodatagrid.ItemsSource = volo.getFlights();
            this.gridCorrente = 5;
            currentGrid();
            partenzapicker.DisplayDateStart = DateTime.Today;
            arrivopicker.DisplayDateStart = DateTime.Today;
            partenzapicker1.DisplayDateStart = DateTime.Today;
            arrivopicker1.DisplayDateStart = DateTime.Today;
            partenzapicker2.DisplayDateStart = DateTime.Today;
            arrivopicker2.DisplayDateStart = DateTime.Today;

        }
        private void Click_profilo(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 4;
            pulisciDipendente();
            abilitaVisioneDipendente();
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
            else
            {
                MessageBox.Show(errore.codError());
            }
        }

        private void txtPartenza_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void rdbDipendenteNome_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdbDipendenteCodice_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void click_applicaOfferta(object sender, RoutedEventArgs e)
        {
            errore.ValueText(txtPsconto);
            if (errore.checkText())
            {

            }
            else
            {
                MessageBox.Show(errore.codError());
            }
        }

        private void txtPartenza_LostFocus(object sender, RoutedEventArgs e)
        {
            lblErroreP.Visibility = !volo.checkDeparture(txtPartenza.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void txtDestinazioneVolo_LostFocus(object sender, RoutedEventArgs e)
        {
            lblErroreD.Visibility = !volo.checkArrival(txtDestinazioneVolo.Text) ? Visibility.Visible : Visibility.Hidden;
        }



        private void dataPartenza_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try { lblErroreDP.Visibility = !volo.checkDateFlight(dataPartenza.SelectedDate.Value) ? Visibility.Visible : Visibility.Hidden; }
            catch { }

        }

        private void dataRitorno_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try { lblErroreDA.Visibility = !volo.checkDateFlight(dataRitorno.SelectedDate.Value) ? Visibility.Visible : Visibility.Hidden; } catch { }

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }


        private void cbVenti_Checked(object sender, RoutedEventArgs e)
        {
            lblpesoBagaglio.Content = lblpesoBagaglio.Content.ToString() + "20 kg, ";
        }

        private void cbTrenta_Checked(object sender, RoutedEventArgs e)
        {
            lblpesoBagaglio2.Content = lblpesoBagaglio.Content.ToString() + "30kg, ";
        }

        private void cbCinquanta_Checked(object sender, RoutedEventArgs e)
        {
            lblpesoBagaglio3.Content = lblpesoBagaglio.Content.ToString() + "50kg";
        }

        private void cbVenti_Unchecked(object sender, RoutedEventArgs e)
        {
            lblpesoBagaglio.Content = "";
        }

        private void cbTrenta_Unchecked(object sender, RoutedEventArgs e)
        {
            lblpesoBagaglio2.Content = "";
        }

        private void cbCinquanta_Unchecked(object sender, RoutedEventArgs e)
        {
            lblpesoBagaglio3.Content = "";
        }



        #endregion

        #region gestione voli

        private void aggiungi_voloClick(object sender, RoutedEventArgs e)
        {
            errore.ValueText(aereoporto_partenzatxt);
            errore.ValueText(aereoporto_arrivotxt);
            errore.checkString(tpPartenza.Text);
            errore.checkString(tpArrivo.Text);
            errore.ValueText(tariffatxt);
            errore.checkNumber(tariffatxt);
            if (errore.checkText())
            {
                if (dipendentevolo.checkPianodivoloExist(aereoporto_partenzatxt.Text, aereoporto_arrivotxt.Text, partenzapicker.SelectedDate.Value.ToString("yyyy-MM-dd"), arrivopicker.SelectedDate.Value.ToString("yyyy-MM-dd"), tpPartenza.Text + ":00", tpArrivo.Text + ":00"))
                {
                    MessageBox.Show("Piano di volo già esistente");
                }
                else
                {
                    if (dipendentevolo.checkAeroporto(aereoporto_partenzatxt.Text).Equals(false))
                    {
                        MessageBox.Show("L'aeroporto " + aereoporto_partenzatxt.Text + " non è presente nel database");
                    }
                    else if (dipendentevolo.checkAeroporto(aereoporto_arrivotxt.Text).Equals(false))
                    {
                        MessageBox.Show("L'aeroporto " + aereoporto_arrivotxt.Text + " non è presente nel database");
                    }
                    else
                    {
                        dipendentevolo.Aggiungi_pianodivolo(aereoporto_partenzatxt.Text, aereoporto_arrivotxt.Text, (DateTime)partenzapicker.SelectedDate, (DateTime)arrivopicker.SelectedDate, tpPartenza.Text + ":00", tpArrivo.Text + ":00", false, Double.Parse(tariffatxt.Text));


                        GridAggiungiPianoDiVolo.Visibility = Visibility.Hidden;
                        GridAggiungiTratta1.Visibility = Visibility.Visible;
                        if (scalichk.IsChecked == true)
                        {
                            if (partenzapicker.SelectedDate == arrivopicker.SelectedDate)
                            {
                                arrivopicker1.SelectedDate = arrivopicker.SelectedDate;
                                partenzapicker2.SelectedDate = partenzapicker.SelectedDate;
                            }
                            aereicbx1.ItemsSource = dipendentevolo.getAerei();//Popula la combobox aerei
                            aereoporto_partenza1txt.Text = aereoporto_partenzatxt.Text;
                            partenzapicker1.SelectedDate = partenzapicker.SelectedDate;
                            tpPartenza1.Text = tpPartenza.Text;

                            aereoporto_arrivo2txt.Text = aereoporto_arrivotxt.Text;
                            arrivopicker2.SelectedDate = arrivopicker.SelectedDate;
                            tpArrivo2.Text = tpArrivo.Text;
                        }
                        else
                        {
                            aereoporto_arrivo1txt.Text = aereoporto_arrivotxt.Text;
                            arrivopicker1.SelectedDate = arrivopicker.SelectedDate;
                            tpArrivo1.Text = tpArrivo.Text;
                        }
                        aereicbx1.ItemsSource = dipendentevolo.getAerei();//Popula la combobox aerei
                        aereoporto_partenza1txt.Text = aereoporto_partenzatxt.Text;
                        partenzapicker1.SelectedDate = partenzapicker.SelectedDate;
                        tpPartenza1.Text = tpPartenza.Text;
                    }


                }
            }
            else
            {
                MessageBox.Show(errore.codError());
            }

        }

        private void aggiungi_tratta1Click(object sender, RoutedEventArgs e)
        {
            errore.ValueText(aereoporto_partenza1txt);
            errore.ValueText(aereoporto_arrivo1txt);
            errore.checkString(tpPartenza1.Text);
            errore.checkString(tpArrivo1.Text);
            errore.ValueText(gatepartenza1txt);
            errore.ValueText(gatearrivo1txt);
            errore.checkString(aereicbx1.Text);

            if (errore.checkText())
            {
                dipendentevolo.Aggiungi_tratta(aereoporto_partenza1txt.Text, aereoporto_arrivo1txt.Text, Convert.ToInt32(gatepartenza1txt.Text), Convert.ToInt32(gatearrivo1txt.Text), (DateTime)partenzapicker1.SelectedDate, (DateTime)arrivopicker1.SelectedDate, tpPartenza1.Text + ":00", tpArrivo1.Text + ":00", aereicbx1.SelectedValue.ToString().Split(null), dipendentevolo.getLastNumero_volo());
                if (scalichk.IsChecked == true)
                {
                    GridAggiungiTratta2.Visibility = Visibility.Visible;
                    aereicbx2.ItemsSource = dipendentevolo.getAerei();//Popula la combobox aerei
                    aereoporto_partenza2txt.Text = aereoporto_arrivo1txt.Text;
                    partenzapicker2.SelectedDate = arrivopicker1.SelectedDate;
                }
                else
                {
                    errore.TraverseVisualTree(this.grid);
                    GridAggiungiTratta1.Visibility = Visibility.Hidden;
                    GridAggiungiPianoDiVolo.Visibility = Visibility.Visible;
                    pianidivolodatagrid.ItemsSource = volo.getFlights();
                    pianidivolodatagrid.Items.Refresh();


                }
            }
            else
            {
                MessageBox.Show(errore.codError());
            }

        }

        private void aggiungi_tratta2Click(object sender, RoutedEventArgs e)
        {
            errore.ValueText(aereoporto_partenza2txt);
            errore.ValueText(aereoporto_arrivo2txt);
            errore.checkString(tpPartenza2.Text);
            errore.checkString(tpArrivo2.Text);
            errore.ValueText(gatepartenza2txt);
            errore.ValueText(gatearrivo2txt);
            errore.checkString(aereicbx2.Text);
            if (errore.checkText())
            {
                dipendentevolo.Aggiungi_tratta(aereoporto_partenza2txt.Text, aereoporto_arrivo2txt.Text, Convert.ToInt32(gatepartenza2txt.Text), Convert.ToInt32(gatearrivo2txt.Text), (DateTime)partenzapicker2.SelectedDate, (DateTime)arrivopicker2.SelectedDate, tpPartenza2.Text + ":00", tpArrivo2.Text + ":00", aereicbx2.SelectedValue.ToString().Split(null), dipendentevolo.getLastNumero_volo());
                errore.TraverseVisualTree(this.grid);
                GridAggiungiTratta1.Visibility = Visibility.Hidden;
                GridAggiungiTratta2.Visibility = Visibility.Hidden;
                GridAggiungiPianoDiVolo.Visibility = Visibility.Visible;
                pianidivolodatagrid.ItemsSource = volo.getFlights();
                pianidivolodatagrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show(errore.codError());
            }

        }

        private void elimina_voloClick(object sender, RoutedEventArgs e)
        {
            volo.CancelFlight(int.Parse(getCellValue(pianidivolodatagrid, 0)));
            pianidivolodatagrid.ItemsSource = volo.getFlights();
            pianidivolodatagrid.Items.Refresh();
        }

        private void dataGridAndata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gridTipoVolo.Visibility = Visibility.Visible;
            
        }

        private void Click_gestioneofferte(object sender, RoutedEventArgs e)
        {
            errore.TraverseVisualTree(this.grid);
            this.gridCorrente = 10;
            currentGrid();
            rdblastminute.IsChecked = true;

        }

        private void rdblastminute_Checked(object sender, RoutedEventArgs e)
        {
            dgVolifiltrati.ItemsSource = volo.getLastMinute();
        }

        private void rdbnatale_Checked(object sender, RoutedEventArgs e)
        {
            dgVolifiltrati.ItemsSource = volo.getXmasFlights();
        }

        private void rdbferragosto_Checked(object sender, RoutedEventArgs e)
        {
            dgVolifiltrati.ItemsSource = volo.getSummerBankHolidayFlights();
        }

        private void applicaSconto_Click(object sender, RoutedEventArgs e)
        {
            int sconto = rdblastminute.IsChecked.Value == true ? 1 : rdbnatale.IsChecked.Value == true ? 2 : 3;
            volo.addDiscount(int.Parse(getCellValue(dgVolifiltrati, 0)), sconto);
            dipendentevolo.UpdateFlightFare(int.Parse(getCellValue(dgVolifiltrati, 0)));
            dgVolifiltrati.ItemsSource = sconto == 1 ? volo.getLastMinute() : sconto == 2 ? volo.getXmasFlights() : volo.getSummerBankHolidayFlights();
        }

        private void MIGestioneDipendenteVoli_Click(object sender, RoutedEventArgs e)
        {
            this.gridCorrente = 11;
            currentGrid();
            dgTratte.ItemsSource = volo.getFlightsLegs();
            dgPersonaleDiVolo.ItemsSource = dipendente.getStaff();

        }

        private void btnAssocia_Click(object sender, RoutedEventArgs e)
        {

            bool flag = dipendentevolo.CombineEmplyerToFlight(int.Parse(getCellValue(dgPersonaleDiVolo, 0)),
                DateTime.Parse(getCellValue(dgTratte, 1)),
                TimeSpan.Parse(getCellValue(dgTratte, 0)),
                int.Parse(getCellValue(dgTratte, 2))
                );
            if (!flag)
            {
                MessageBox.Show("Dipendente già in volo");
            }
            else
            {
                dgPersonaleInVolo.ItemsSource = dipendentevolo.getEmployersInFly(
                DateTime.Parse(getCellValue(dgTratte, 1)),
                TimeSpan.Parse(getCellValue(dgTratte, 0)),
                int.Parse(getCellValue(dgTratte, 2)));
            }
        }

        private void dgTratte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*dgPersonaleInVolo.ItemsSource = dipendentevolo.getEmployersInFly(
               DateTime.Parse(getCellValue(dgTratte, 1)),
               TimeSpan.Parse(getCellValue(dgTratte, 0)),
               int.Parse(getCellValue(dgTratte, 2)));*/
        }


        private void pianidivolodatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnElimina_volo.IsEnabled = true;
        }



        #endregion

        #endregion

        #region utility methods
        String getCellValue(DataGrid dg, int index)
        {
            return (dg.SelectedCells[index].Column.GetCellContent(dg.SelectedItem) as TextBlock).Text;
        }

        #endregion

    }

}
