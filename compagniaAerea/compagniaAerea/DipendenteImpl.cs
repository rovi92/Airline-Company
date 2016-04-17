using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    class DipendenteImpl : Dipendente
    {
        DatabaseManager myDatatabase;
        InfoDipendente id = new InfoDipendente();
        public DipendenteImpl()
        {
            myDatatabase = DatabaseManager.Instance;
        }

        public class InfoDipendente
        {
            public string nome { get; set; }
            public string cognome { get; set; }
            public string indirizzo { get; set; }
            public DateTime data_nascita { get; set; }
            public DateTime data_assunzione { get; set; }
            public string email { get; set; }
            public string telefono { get; set; }
            public string sesso { get; set; }
            public bool pilota { get; set; }
            public bool hostess { get; set; }
        }

        public void dipendente(string nome_dipendente, string cognome_dipendente, int idDipendente)
        {
            id = (from d in myDatatabase.getDb().Personale
                  where d.nome == nome_dipendente && d.cognome == cognome_dipendente
                  select new InfoDipendente
                  {
                      nome = d.nome,
                      cognome = d.cognome,
                      indirizzo = d.indirizzo,
                      data_nascita = d.data_di_nascita,
                      data_assunzione = d.data_assunzione,
                      email = d.email,
                      telefono = d.telefono,
                      sesso = d.sesso,
                      pilota = d.pilota,
                      hostess = d.hostess
                  }).First();
           
        }

        #region get Dipendente
        public string getCognome()
        {
            return id.nome;
        }

        public string getNome()
        {
            return id.cognome;
        }

        public string getIndirizzo()
        {
            return id.indirizzo;
        }

        public string getDataNascita()
        {
            return id.data_nascita.ToString("dd/MM/yyyy");
        }

        public string getDataAssunzione()
        {
            return id.data_assunzione.ToString("dd/MM/yyyy");
        }

        public string getEmail()
        {
            return id.email;
        }

        public string getTelefono()
        {
            return id.telefono;
        }

        public string getSesso()
        {
            return id.sesso;
        }
        /*per pilota o hostess ritorna il nome della categoria se true altrimenti stringa vuota*/
        public string getPilota()
        {
            if (id.pilota == true)
            {
                return "Pilota";
            }
            else
            {
                return "";
            }

        }

        public string getHostess()
        {
            if (id.hostess == true)
            {
                return "Hostess";
            }
            else
            {
                return "";
            }
        }

        #endregion
        #region set Dipendente
       
        public void setIndirizzo(string indirizzo)
        {
            throw new NotImplementedException();
        }

        public void setEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void setTelefono(string telefono)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
