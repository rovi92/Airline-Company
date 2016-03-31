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
        #region registrazione cliente
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

        #region controllo CF
        public Boolean controlCF(String CF)
        {
            Boolean i = (from c in myDatabase.Passeggero
                         select c.CF).Contains(CF);
            if (i.Equals(true))
            {
                MessageBox.Show("codice fiscale non valido");//entra qui solo se esiste
                return false;
            }

            return true;
        }
        #endregion
       
        #region controllo username
        public Boolean controlloUsername(String username)
        {
            Boolean i = (from c in myDatabase.Passeggero
                         select c.Username).Contains(username);
            if (i.Equals(true))
            {
                MessageBox.Show("username non valido");
                return false;
            }
            return true;
        }
        #endregion
      
        #region controllo mail
        public Boolean controlloEmail(String email)
        {
            Boolean i = (from c in myDatabase.Passeggero
                         select c.Email).Contains(email);
            if (i.Equals(true))
            {
                MessageBox.Show("mail già registrata");
                return false;
            }
            return true;
        }
        #endregion
      
        
        #endregion

        #region ricerca del volo
        /* public dynamic Cerca_volo(String partenza, String destinazione, DateTime data_partenza, DateTime data_ritorno)
         {
             var cerca_volo = (from p in myDatabase.Piano_di_volo
                               join aeroporto in myDatabase.Aereoporto on p.Aereoporto_partenza equals aeroporto.Nome
                               join tariffario in myDatabase.Tariffario on p.idPiano_di_volo  equals tariffario.idPiano_di_volo
                               where p.idPiano_di_volo == 3
                          select new*/
        /*  {
             Prova = p.Partenza,
              Arrivo = p.Arrivo
              p.Partenza,
              p.Arrivo,
              p.Data_partenza,
              p.Orario_partenza,
              p.Data_arrivo,
              p.Orario_arrivo,
              aeroporto.Indirizzo

          });

return cerca_volo;


//Console.WriteLine("VEDIAMO COSA SCRIVE STA ROBA "+prova.ToString());
}
*/
        #endregion

        #region controllo cliente
        public Boolean statoRegistrazioneCliente()
        {
            return this.statoRegistrazione;
        }
        #endregion
    }
}
