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
        DipendenteVoloImpl dipendentevolo;
        InterfacciaError errore = new Error();
        Ticket ticket = new TicketImpl();
        Dipendente dipendente = new DipendenteImpl();

        public MainWindow()
        {

            gestione_cliente = new Gestione_utente();// la classe può esssere richiamata anche sotto se si vuole
            volo = new VoloImpl();//classe volo
            dipendentevolo = new DipendenteVoloImpl();//classe dipendente volo (per la gestione dei voli da parte del dipendente)
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
                    MessageBox.Show("registrazione avvenuta con successo");
                    this.gridCorrente = 2;
                    volo.executeTratta();
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
            if (!btnConferma_ordine.IsVisible)
            {
                btnConferma_ordine.Visibility = Visibility.Visible;
                ticket.setQuantitàPersone(Int32.Parse(lblPosti.Content.ToString()));
            }
            btnIndietro.Visibility = Visibility.Hidden;
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
            volo.setClass(rdbEconomy.Content.ToString());

        }
        private void rdbBuisness_Checked(object sender, RoutedEventArgs e)
        {
            volo.setClass(rdbBuisness.Content.ToString());
        }
        private void rdbFirst_Checked(object sender, RoutedEventArgs e)
        {
            volo.setClass(rdbFirst.Content.ToString());
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
            else
            {
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
                /*else
                {
                    dipendente.dipendente(Login_usernametxt.Text);
                    if (Login_usernametxt.Text.Equals(dipendente.getNome()))
                    {
                        this.gridCorrente = 4;
                        currentGrid();
                    }
                }*/
            }
            else
            {
                MessageBox.Show("Riempire tutti i campi", "ERRORE!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region grid infoVolo dopo il prenota

        private void ckbpranzo_Checked(object sender, RoutedEventArgs e)
        {
            int sum = Int32.Parse(txtSommaConfort.Text) + 20;
            txtSommaConfort.Text = Convert.ToString(sum);
        }
        private void ckbpranzo_Unchecked(object sender, RoutedEventArgs e)
        {
            int sumConfort = Int32.Parse(txtSommaConfort.Text) - 20;
            if (sumConfort <= 0)
            {
                sumConfort = 0;
            }
            txtSommaConfort.Text = Convert.ToString(sumConfort);
        }
        private void ckbAcon_Unchecked(object sender, RoutedEventArgs e)
        {
            int sum = Int32.Parse(txtSommaConfort.Text) - 5;
            txtSommaConfort.Text = Convert.ToString(sum);
        }

        private void ckbAcon_Checked(object sender, RoutedEventArgs e)
        {
            int sumConfort = Int32.Parse(txtSommaConfort.Text) + 5;
            if (sumConfort <= 0)
            {
                sumConfort = 0;
            }
            txtSommaConfort.Text = Convert.ToString(sumConfort);
        }

        private void btnConfermaDati_Click(object sender, RoutedEventArgs e)
        {
            errore.ValueText(nomepasseggerotxt);
            errore.ValueText(cognomepasseggerotxt);
            errore.IsValidEmail(emailpasseggerotxt);
            errore.ValueText(viapasseggerotxt);
            errore.CAPCheck(cappasseggerotxt, cappasseggerotxt.Text, 5);
            
            if (errore.checkText())
            {
                this.gridCorrente = 8;
                currentGrid();
                if (btnConferma_ordine.Visibility.Equals(Visibility.Hidden))
                {
                    btnConferma_ordine.Visibility = Visibility.Visible;
                }
                ticket.getPopulateDbTicket();
                nomelbl.Content = nomepasseggerotxt.Text;
                cognomelbl.Content = cognomepasseggerotxt.Text;
                CFlbl.Content = cfpasseggerotxt.Text;
                codiceVololbl.Content = volo.getValueGrid(dataGridAndata)[5];
                aereoporteAndatalbl.Content = volo.getNameAirport(volo.getValueGrid(dataGridAndata)[0]);
                aereoportoArrivolbl.Content = volo.getNameAirport(volo.getValueGrid(dataGridAndata)[1]);
                oraPartenzalbl.Content = volo.getValueGrid(dataGridAndata)[3];
                oraArrivolbl.Content = volo.getValueGrid(dataGridAndata)[4];
                dataPartenzalbl.Content = dataPartenza.SelectedDate.ToString();
                ComboBoxItem typeItem = (ComboBoxItem)tipobabgliocombobox.SelectedItem;
                string kgBagaglio = typeItem.Content.ToString();
                totalelbl.Content = (ticket.getTotal(Convert.ToDouble(kgBagaglio), Convert.ToDouble(bagaglitxt.Text), Convert.ToDouble(codiceVololbl.Content.ToString()), Convert.ToDouble(txtSommaConfort.Text), volo.getClassId()) * int.Parse(lblPosti.Content.ToString())).ToString();

            }
            else
            {
                MessageBox.Show(errore.codError());
            }
        }

        private void conferma_ordine_click(object sender, RoutedEventArgs e)
        {
            if (rdbAndataRitorno.IsChecked.Value)
            {

                ComboBoxItem typeItem = (ComboBoxItem)tipobabgliocombobox.SelectedItem;
                string stato = typeItem.Content.ToString();

                ticket.firstTicket(new List<string>
                {
                    nomelbl.Content.ToString(),
                    cognomelbl.Content.ToString(),
                    CFlbl.Content.ToString(),
                    codiceVololbl.Content.ToString(),
                    aereoporteAndatalbl.Content.ToString(),
                    aereoportoArrivolbl.Content.ToString(),
                    oraPartenzalbl.Content.ToString(),
                    oraArrivolbl.Content.ToString(),
                    dataPartenzalbl.Content.ToString(),
                    stato,
                    totalelbl.Content.ToString()
                });
                nomelbl.Content = nomepasseggerotxt.Text;
                cognomelbl.Content = cognomepasseggerotxt.Text;
                CFlbl.Content = cfpasseggerotxt.Text;
                codiceVololbl.Content = volo.getValueGrid(dataGridRitorno)[5];
                aereoporteAndatalbl.Content = volo.getNameAirport(volo.getValueGrid(dataGridRitorno)[0]);
                aereoportoArrivolbl.Content = volo.getNameAirport(volo.getValueGrid(dataGridRitorno)[1]);
                oraPartenzalbl.Content = volo.getValueGrid(dataGridRitorno)[3];
                oraArrivolbl.Content = volo.getValueGrid(dataGridRitorno)[4];
                dataPartenzalbl.Content = dataPartenza.SelectedDate.ToString();
                totalelbl.Content = ticket.getTotal(Convert.ToDouble(stato), Convert.ToDouble(bagaglitxt.Text), Convert.ToDouble(codiceVololbl.Content.ToString()), Convert.ToDouble(txtSommaConfort.Text), volo.getClassId()) * int.Parse(lblPosti.Content.ToString());
                btnConferma2.Visibility = Visibility.Visible;
                btnConferma_ordine.Visibility = Visibility.Hidden;



            }
            else
            {
                txtdataPagamento.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtTotale.Text = totalelbl.Content.ToString();
                this.gridCorrente = 9;
                currentGrid();
            }
        }

        private void btnConferma2_Click(object sender, RoutedEventArgs e)
        {

            txtdataPagamento.Text = DateTime.Today.ToString("yyyy-MM-dd");

            txtTotale.Text = Convert.ToString(Convert.ToDouble(ticket.getFirstTicket()[10]) + Double.Parse(totalelbl.Content.ToString()));



            this.gridCorrente = 9;
            currentGrid();
        }


        #endregion

        #region PAGAMENTO
        private void btnConferma3_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbTipoPagamento.SelectedItem;
            string tipoPagamento = item.Content.ToString();
            if (rdbSoloAndata.IsChecked.Value)
            {
                if (gestione_cliente.controlCF(lblPosti.Content.ToString()).Equals(true) || gestione_cliente.controlloEmail(emailpasseggerotxt.Text).Equals(true))
                {
                    gestione_cliente.InitUtente();
                    ticket.getPopulateDbTicket();

                    ticket.createBooking(txtdataPagamento.Text,
                    int.Parse(lblPosti.Content.ToString()),
                    Double.Parse(txtTotale.Text.ToString()),
                    gestione_cliente.getLastIdPassenger(cfpasseggerotxt.Text, emailpasseggerotxt.Text),
                    ticket.getIdTariffa(int.Parse(codiceVololbl.Content.ToString()),
                    volo.getClassId()));

                    ticket.getPopulateDbTicket();
                   /* ticket.insertRecordPagamento(txtdataPagamento.Text, tipoPagamento, ticket.getIdPrenotaione());
                    ticket.insertRecordTiket(gestione_cliente.getLastIdPassenger(cf., cognomepasseggerotxt.Text, ticket.getIdPrenotaione());*/

                }
                else
                {
                    gestione_cliente.InitUtente();
                    gestione_cliente.Registrazione_Cliente(nomepasseggerotxt.Text,
                        cognomepasseggerotxt.Text,
                        viapasseggerotxt.Text,
                        emailpasseggerotxt.Text,
                        cittàpasseggerotxt.Text,
                        Int32.Parse(cappasseggerotxt.Text),
                        cfpasseggerotxt.Text);

                    gestione_cliente.InitUtente();
                    ticket.getPopulateDbTicket();
                    ticket.createBooking(txtdataPagamento.Text,
                    int.Parse(lblPosti.Content.ToString()),
                    Double.Parse(txtTotale.Text.ToString()),
                    gestione_cliente.getLastIdPassenger(cfpasseggerotxt.Text, emailpasseggerotxt.Text),
                    ticket.getIdTariffa(int.Parse(codiceVololbl.Content.ToString()),
                    volo.getClassId()));


                    ticket.getPopulateDbTicket();
                    /*ticket.insertRecordPagamento(txtdataPagamento.Text, tipoPagamento, ticket.getIdPrenotaione());
                    ticket.insertRecordTiket(nomepasseggerotxt.Text, cognomepasseggerotxt.Text, ticket.getIdPrenotaione());*/
                }

            }
            else
            {
              if (gestione_cliente.controlCF(lblPosti.Content.ToString()).Equals(true) || gestione_cliente.controlloEmail(emailpasseggerotxt.Text).Equals(true))
                {
                   /* gestione_cliente.InitUtente();
                    ticket.getPopulateDbTicket();
                    ticket.createBooking(txtdataPagamento.Text,
                    
                        );*/
                    /*  ticket.createBooking(txtdataPagamento.Text,
                    int.Parse(lblPosti.Content.ToString()),
                    Double.Parse(txtTotale.Text.ToString()),
                    gestione_cliente.getLastIdPassenger(cfpasseggerotxt.Text, emailpasseggerotxt.Text),
                    ticket.getIdTariffa(int.Parse(codiceVololbl.Content.ToString()),
                    volo.getClassId()));*/
                }
                else
                {

                }
            }
            MessageBox.Show("grazie per aver scelto la nostra compagnia");

            this.gridCorrente = 2;
            currentGrid();
            ticket.getPopulateDbTicket();
            volo.executeTratta();
            errore.TraverseVisualTree(this.grid);
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
                dipendentevolo.Aggiungi_pianodivolo((DateTime) partenzapicker.SelectedDate, (DateTime) arrivopicker.SelectedDate, orario_partenzatxt.Text, orario_arrivotxt.Text, false);
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
