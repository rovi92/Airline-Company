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
    class Gestione_utente 
    {
        
        DatabaseManager myDatabase;
        Boolean statoRegistrazione = false;
        ArrayList alErrori = new ArrayList();
        public Gestione_utente()
        {
           
            myDatabase = DatabaseManager.Instance;
            
          
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
                    nome = nome,
                    cognome = cognome,
                    data_di_nascita = data_di_nascita,
                    username = username,
                    password = password,
                    telefono = telefono,
                    indirizzo = indirizzo,
                    email = email,
                    stato = stato,
                    regione = regione,
                    città = città,
                    CAP = CAP,
                    CF = CF,
                };
                myDatabase.getDb().Passeggero.InsertOnSubmit(p);
                myDatabase.getDb().SubmitChanges();

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
            Boolean i = (from c in myDatabase.getDb().Passeggero
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
            Boolean i = (from c in myDatabase.getDb().Passeggero
                         select c.username).Contains(username);
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
            Boolean i = (from c in myDatabase.getDb().Passeggero
                         select c.email).Contains(email);
            if (i.Equals(true))
            {
                MessageBox.Show("mail già registrata");
                return false;
            }
            return true;
        }
        #endregion
      
        
        #endregion

        #region controllo cliente
        public Boolean statoRegistrazioneCliente()
        {
            return this.statoRegistrazione;
        }
        #endregion
    }
}
