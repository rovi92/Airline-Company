using System;
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

            }
            else
            {
                MessageBox.Show("password errata");
            }
        }
    }
}
