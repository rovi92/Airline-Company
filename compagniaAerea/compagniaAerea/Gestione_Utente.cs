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
        List<Passeggero> registrazioneUtente = new List<Passeggero>();
        ArrayList alErrori = new ArrayList();
        public Gestione_utente()
        {

            myDatabase = DatabaseManager.Instance;


        }
        public void InitUtente()
        {
            registrazioneUtente = (from ru in myDatabase.getDb().Passeggero
                                   select ru).ToList();
        }
        #region registrazione cliente
        //metodo per registrazione
        public void Registrazione_Cliente(String nome, String cognome, DateTime data_di_nascita, String username, String password, String password2, String indirizzo, String telefono, String email, String stato, String regione, String città, int CAP, String CF)
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
            Boolean flag = true;
            foreach (Passeggero p in registrazioneUtente)
            {
                if (p.CF.Equals(CF))
                {
                    flag = false;
                }
            }
            if (!flag)
            {
                MessageBox.Show("codice fiscale non valido");
            }
            return flag;
        }
        #endregion

        #region controllo username
        public Boolean controlloUsername(String username)
        {
            Boolean flag = true;
            foreach (Passeggero p in registrazioneUtente)
            {
                if (p.username.Equals(username))
                {
                    flag = false;
                }
                
            }
            if (!flag)
            {
                MessageBox.Show("username non valido");

            }
            return flag;
        }
        #endregion

        #region controllo mail
        public Boolean controlloEmail(String email)
        {
            Boolean flag = true;
            foreach (Passeggero p in registrazioneUtente)
            {
                if (p.email.Equals(email))
                {
                   flag = false;
                }
            }
            if (!flag)
            {
                MessageBox.Show("email già utilizzata");
            }
            return flag;
        }

        #region controllo cliente
        public Boolean statoRegistrazioneCliente()
        {
            return this.statoRegistrazione;
        }
        #endregion
    }
}
        #endregion


#endregion



