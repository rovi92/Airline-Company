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
            public string nome;
            public string cognome;
            //public string psw;
        }

        public void getDipendente(string nome_dipendente, string cognome_dipendente/*, string psw_dipendente*/)
        {
            id = (from d in myDatatabase.getDb().Personale
                  where d.nome == nome_dipendente && d.cognome == cognome_dipendente
                  select new InfoDipendente
                  {
                      nome = d.nome,
                      cognome=d.cognome,
                      /*psw = d.password o quello che sarà*/
                  }).First();
        }

        public string getCognome()
        {
            return id.nome;
        }

        public string getNome()
        {
            return id.cognome;
        }

        public string getPsw()
        {
            return "";
        }
    }
}
