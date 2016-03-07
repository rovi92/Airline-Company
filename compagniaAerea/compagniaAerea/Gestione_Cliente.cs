using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    class Gestione_Cliente
    {
        String stringa_connessione;
        SqlConnection con;//con sta per connessione
        SqlCommand cmd;//cmd sta per command

        public Gestione_Cliente(String stringa_connessione)
        {
            this.stringa_connessione = stringa_connessione;
        }

        public void Registrazione_Cliente(String nome, String cognome, DateTime data_di_nascita, String username, String password, String indirizzo, String telefono, String email, String stato, String regione, String città, int CAP, String CF)
        {
            con = new SqlConnection(stringa_connessione);
            string query = "INSERT INTO [Clienti] (Nome, Cognome, Data_di_nascita, Username, Password, Indirizzo, Telefono, Email, Stato, Regione, Città, CAP, CF) VALUES('" + nome + "','" + cognome + "','" + data_di_nascita + "','" + username + "','" + password + "','" + indirizzo + "','" + telefono + "','" + email + "','" + stato + "','" + regione + "','" + città + "','" + CAP + "','" + CF + "')";
            cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
       
        }
    }
}
