using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace compagniaAerea
{

    class TicketImpl : Ticket
    {

        DatabaseManager myDatabase;
      //  ListCollectionView biglietti;
       List<String> primoTicket = new List<String>();
        List<Comfort> comfort = new List<Comfort>();
        List<Classe> classeVolo = new List<Classe>();
        List<Tariffario> tariffario = new List<Tariffario>();
        List<Prezzo_bagaglio_imbarcato> bagaglio = new List<Prezzo_bagaglio_imbarcato>();
      

        public TicketImpl()
        {
            myDatabase = DatabaseManager.Instance;
        }

        public void getBiglitti(int codiceBiglietto)
        {
            var query = (from b in myDatabase.getDb().Biglietto
                             //  where b.codice_biglietto.Equals(codiceBiglietto)
                         select new
                         {
                             cod_biglietto = b.codice_biglietto,//indice 0
                             nome_intestatario = b.nome_intestatario,//indeice 1
                             cognome_intestatario = b.cognome_intestatario,//ecc..
                             cod_fiscale = b.Prenotazione.Passeggero.CF,
                             cod_volo = b.Prenotazione.Tariffario.numero_volo,
                             aerep_andata = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_partenza,
                             aerep_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_arrivo,
                             ora_partenza = b.Prenotazione.Tariffario.Piano_di_volo.orario_partenza,
                             ora_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.orario_arrivo,
                             data_partenza = b.Prenotazione.Tariffario.Piano_di_volo.data_partenza,
                             data_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.data_arrivo,

                             prezzo_bagaglio = b.Babaglio_Imbarcato.Where(bi => bi.peso <= bi.Prezzo_bagaglio_imbarcato.range_pesi).First().Prezzo_bagaglio_imbarcato.prezzo + (
                                                                                                                                    b.Prenotazione.Tariffario.tariffa_solo_andata *
                                                                                                                                    b.Prenotazione.Tariffario.Classe.prezzo)
                            // predadd = b.Prenotazione.Tariffario.Comfort_inclusi.Where(c => c.idTariffa == b.Prenotazione.idTariffa).First().Comfort.prezzo
                         }).ToList();
          
        }
        public void getPopulateDbTicket()
        {
        

            tariffario = (from t in myDatabase.getDb().Tariffario
                          select t).ToList();
            classeVolo = (from cll in myDatabase.getDb().Classe
                          select cll).ToList();

            bagaglio = (from pbi in myDatabase.getDb().Prezzo_bagaglio_imbarcato
                        select pbi).ToList();

            comfort = (from c in myDatabase.getDb().Comfort
                       select c).ToList();
        }
/*
        public String getNome(int codiceBiglietto)
        {
            string nome = "";
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) )
                //controllo corrispondenza tra nome cognome e codice inseriti da utente e quelli presenti nel DB
                {
                    nome = b.nome_intestatario;
                }
                break;
            }
            return nome;         
        }
        public String getCognome(int codiceBiglietto)
        {
            string cognome = "";
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))

                {
                    cognome = b.cognome_intestatario;
                }
                break;
            }
            return cognome;
        }
        public String getCF(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            string CF = "";
           
                foreach (Biglietto b in biglietti)
                {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))

                    {
                        CF = b.Prenotazione.Passeggero.CF;
                    }
                    break;
                }
                return CF;
            }
        public Int32 getCodiceVolo(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            int cod_volo = 0;
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                {
                    cod_volo = b.Prenotazione.Tariffario.numero_volo;
                }
                break;
            }
            return cod_volo;
        }
        public String getAereoportoAndata(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            string andata = "";
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                {
                    andata = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_partenza;
                }
                break;
            }
            return andata;
        }
        public String getAereoportoArrivo(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            string ritorno = "";
                foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                {
                    ritorno = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_arrivo;
                }
                break;
            }
            return ritorno;
        }
        public TimeSpan getOraPartenza(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            TimeSpan ora = new TimeSpan(00, 00, 00, 00);//giorni, ore , minuti, secondi
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                {
                    ora = b.Prenotazione.Tariffario.Piano_di_volo.orario_partenza;
                }
                break;
            }
           
            return ora;
        }
        public TimeSpan getOraArrivo(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            TimeSpan ora = new TimeSpan(00, 00, 00, 00);//giorni, ore , minuti, secondi
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                {
                    ora = b.Prenotazione.Tariffario.Piano_di_volo.orario_arrivo;
                }
                break;
            }
                            
            return ora;
        }
        public DateTime getDataPartenza(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            DateTime data = new DateTime(2016, 1, 1);
           foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                {
                    data = b.Prenotazione.Tariffario.Piano_di_volo.data_partenza;
        }
                break;
        }
            return data;
        }
        public DateTime getDataArrivo(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            DateTime data = new DateTime(2016, 1, 1);
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                {
                    data = b.Prenotazione.Tariffario.Piano_di_volo.data_arrivo;
        }
                break;
        }
            return data;
        }
        public Double getSpesaTotale(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            double spesa = 0;
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                {
                    spesa = (b.Babaglio_Imbarcato.Where(bi => bi.peso <= bi.Prezzo_bagaglio_imbarcato.range_pesi).First().Prezzo_bagaglio_imbarcato.prezzo)
                                                                                                        + ((b.Prenotazione.Tariffario.tariffa_solo_andata +
                                                                                                        b.Prenotazione.Tariffario.Comfort_inclusi.Where(ci => ci.idComfort <= ci.Comfort.idComfort).First().Comfort.prezzo) *
                                                                                                        b.Prenotazione.Tariffario.Classe.prezzo);
        }
                break;
        }
            return spesa;
        }
         public void DatiAnagraficiBiglietto(TextBox tb)
        {
            throw new NotImplementedException();
        }
*/
        public double getTotal(double kg,double quantitaBagagli, double numeroVolo, double confort, string classe)
        {
            double prezzoBagaglio = 0;
            for (int i = 0; i < bagaglio.Count; i++)
            {
                if (bagaglio[i].range_pesi == (kg))
                {
                    prezzoBagaglio = bagaglio[i].prezzo;
                }

            }
            double tariffaSolaAndata = 0;
            for (int a = 0; a < tariffario.Count; a++)
            {
                if (tariffario[a].numero_volo ==(numeroVolo))
                {
                    tariffaSolaAndata = tariffario[a].tariffa_solo_andata;
                }
            }
           
            double classeScelta = 0;
          for(int cl = 0; cl < classeVolo.Count; cl++)
            {
                if (classeVolo[cl].descrizione.Equals(classe))
                {
                    classeScelta = classeVolo[cl].prezzo;
                }
            }
            return (prezzoBagaglio*quantitaBagagli) +( tariffaSolaAndata+ confort)+(classeScelta);
        }

    
        public void firstTicket(List<String> l)
        {
            primoTicket = l;
        }

        public List<string> getFirstTicket()
        {
            return primoTicket;
        }
    }
}
