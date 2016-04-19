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
        int idPasseggero = 0;
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
        public void Registrazione_Cliente(String nome, String cognome, String indirizzo, String email, String città, int CAP, String CF)
        {

            Passeggero p = new Passeggero()
            {
                //inserimento dati nel database
                idPasseggero = 1234,
                nome = nome,
                cognome = cognome,
                indirizzo = indirizzo,
                email = email,
                città = città,
                CAP = CAP,
                CF = CF,
            };
            myDatabase.getDb().Passeggero.InsertOnSubmit(p);
            myDatabase.getDb().SubmitChanges();

            statoRegistrazione = true;
        }
    
           
        

        #region controllo CF
        public Boolean controlCF(String CF)
        {
            Boolean flag = true;
            foreach (Passeggero p in registrazioneUtente)
            {
                if (p.CF.Equals(CF))
                {
                    this.idPasseggero = p.idPasseggero;
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

      
      #region controllo mail
        public Boolean controlloEmail(String email)
        {
            Boolean flag = true;
            foreach (Passeggero p in registrazioneUtente)
            {
                if (p.email.Equals(email))
                {
                    this.idPasseggero = p.idPasseggero;
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

        public int getIdPassenger()
        {
           
            return this.idPasseggero;
        }

        public int getLastIdPassenger(string cf,string email)
        {
            int a = 0;
            int lastId = 0;
            foreach(Passeggero p in registrazioneUtente)
            {
                if(p.CF.Equals(cf) && p.email.Equals(email))
                {
                    a = p.idPasseggero;
                }
                lastId++;
            }
            if(a == 0)
            {
                a = lastId;
            }
            return a;
              // myDatabase.getDb().Passeggeros.First(t => t.idPasseggero.Equals(a)).idPasseggero;
              // aereop_partenza = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_partenza,
        }
    }
}
        #endregion


#endregion



