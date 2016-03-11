using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace compagniaAerea
{
    class Gestione_Cliente
    {
        private myDatabaseUniboAirlineDataContext myDatabase;
        Boolean statoRegistrazione = false;
        ArrayList alErrori = new ArrayList();
        public Gestione_Cliente()
        {
            myDatabase = new myDatabaseUniboAirlineDataContext(); //connessione al database
        }
        //metodo per registrazione
        public void Registrazione_Cliente(String nome, String cognome, DateTime data_di_nascita, String username, String password,String password2, String indirizzo, String telefono, String email, String stato, String regione, String città, int CAP, String CF)
        {
            
            if (password == password2)
            {

                Passeggero p = new Passeggero()
                {
                    //inserimento dati nel database
                    idPasseggero = 1234,
                    Nome = nome,
                    Cognome = cognome,
                    Data_di_nascita = data_di_nascita,
                    Username = username,
                    Password = password,
                    Telefono = telefono,
                    Indirizzo = indirizzo,
                    Email = email,
                    Stato = stato,
                    Regione = regione,
                    Città = città,
                    CAP = CAP,
                    CF = CF,
                };
                myDatabase.Passeggero.InsertOnSubmit(p);
                myDatabase.SubmitChanges();
                statoRegistrazione = true;
            }
            else
            {
                MessageBox.Show("password errata");
                statoRegistrazione = false;
            }
        }

        public dynamic Cerca_volo(String partenza, String destinazione, DateTime data_partenza, DateTime data_ritorno)
        {
            var cerca_volo = (from p in myDatabase.Piano_di_volo
                         where p.idPiano_di_volo == 3
                         select new
                         {
                            /* Prova = p.Partenza,
                             Arrivo = p.Arrivo*/
                             p.Partenza,
                             p.Arrivo,
                             p.Data_partenza,
                             p.Data_arrivo
                         });

            return cerca_volo;


            //Console.WriteLine("VEDIAMO COSA SCRIVE STA ROBA "+prova.ToString());
        }

        public bool erroriDiScrittura(String nome, String cognome, DateTime data_di_nascita, String username, String password, String password2, String indirizzo, String telefono, String email, String stato, String regione, String città, int CAP, String CF)
        {
            //metodo per correggere i campi 
           
            //per ora ho fatto solo l'inserimento dei dati in una array list in maniera che sia piena di true e folse e che le nostre textbox siano legate a un numero identificativo da 1 a... 
            //così facendo gli si può applicare un metodo ciclico che restituisce dove è l'errore e cosa contiene
            String ddNascita = Convert.ToString(data_di_nascita);
            String psw = Convert.ToString(password);
            String psw2 = Convert.ToString(password2);
            #region nome
            alErrori.Add(correctNull(nome,"nome"));
            alErrori.Add(correctNull(cognome,"cognome"));
            alErrori.Add(correctNull(ddNascita, "data di nascita"));
            alErrori.Add(correctNull(username, "username"));
           //password e password2 devono avere un altro tipo di correzzione cioè un metodo per la lunghezza
           //indirizzo non ha nessun tipo di controllo può essere vuoto
           //telefono ha bisogno del controllo numerico e di lunghezza massima
           //e mail non ha bisogno di controllo sui campi ma ha bisogno del controllo dell'esistenza 
            alErrori.Add(correctNull(stato, "stato"));
            alErrori.Add(correctNull(regione, "regione"));
            alErrori.Add(correctNull(città, "città"));
            //il cap e il CF hanno bisogno di avere un controllo di lunghezza come telefono quindi li farò appena posso
            #endregion



            return true;
        }
        public Boolean correctNull(String val,String stVal)
        {
            if (val != "")
            {
                if (val != stVal)
                {
                    return true;
                }
            }
            return false;
        }
        public Boolean statoRegistrazioneCliente()
        {
            return this.statoRegistrazione;
        }
    }
}
