using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
            public int codice { get; set; }
            public string indirizzo { get; set; }
            public DateTime data_nascita { get; set; }
            public DateTime data_assunzione { get; set; }
            public string email { get; set; }
            public string telefono { get; set; }
            public string sesso { get; set; }
            public bool pilota { get; set; }
            public bool hostess { get; set; }
        }

        public void getDipendente(int idDipendente)
        {
            id = (from d in myDatatabase.getDb().Personale
                  where d.idPersonale == idDipendente
                  select new InfoDipendente
                  {
                      nome = d.nome,
                      cognome = d.cognome,
                      codice = d.idPersonale,
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
        public List<Personale> getStaff()
        {
            
            return (from d in myDatatabase.getDb().Personale
                     select d).ToList();
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
        public string getCodice()
        {
            return id.codice.ToString();
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
        public string getImpiego()
        {
            if (id.pilota == true)
            {
                return "Pilota";
            }
            else
             if (id.hostess == true)
            {
                return "Hostess";
            }
            else
                return "Errore";
        }


        #endregion
        #region set Dipendente

        public void createDipendente(string nome, string cognome, string indirizzo, DateTime data_di_nascita, DateTime data_assunzione, string email, string telefono, string sesso, bool pilota, bool hostess)
        {
            Personale p = new Personale()
            {
                nome = nome,
                cognome = cognome,
                indirizzo = indirizzo,
                data_di_nascita = data_di_nascita,
                data_assunzione = data_assunzione,
                email = email,
                telefono = telefono,
                sesso = sesso,
                pilota = pilota,
                hostess = hostess
            };
            myDatatabase.getDb().Personale.InsertOnSubmit(p);
            myDatatabase.getDb().SubmitChanges();
        }

        public void setIndirizzo(string indirizzo, int idPersonale)
        {
            Personale p = myDatatabase.getDb().Personale.First(pr => pr.idPersonale == idPersonale);
            p.indirizzo = indirizzo;
            myDatatabase.getDb().SubmitChanges();
        }

        public void setEmail(string email, int idPersonale)
        {
            Personale p = myDatatabase.getDb().Personale.First(pr => pr.idPersonale == idPersonale);
            p.email = email;
            myDatatabase.getDb().SubmitChanges();
        }

        public void setTelefono(string telefono, int idPersonale)
        {
            Personale p = myDatatabase.getDb().Personale.First(pr => pr.idPersonale == idPersonale);
            p.telefono = telefono;
            myDatatabase.getDb().SubmitChanges();          
        }



        #endregion
    }
}
