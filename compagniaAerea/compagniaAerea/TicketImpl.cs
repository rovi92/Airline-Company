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
        List<String> primoTicket = new List<String>();
        List<Comfort> comfort = new List<Comfort>();
        List<Classe> classeVolo = new List<Classe>();
        List<Tariffario> tariffario = new List<Tariffario>();
        List<Prezzo_bagaglio_imbarcato> bagaglio = new List<Prezzo_bagaglio_imbarcato>();
        InfoBiglietto ib = new InfoBiglietto();


        public TicketImpl()
        {
            myDatabase = DatabaseManager.Instance;
        }

        public class InfoBiglietto
        {
            public string nome { get; set; }
            public string cognome { get; set; }
            public string cod_fiscale { get; set; }
            public int cod_volo { get; set; }
            public string aereop_partenza { get; set; }
            public string aereop_arrivo { get; set; }
            public TimeSpan ora_partenza { get; set; }
            public TimeSpan ora_arrivo { get; set; }
            public DateTime data_partenza { get; set; }
            public DateTime data_arrivo { get; set; }
            public double spesa { get; set; }
        }



        public void getBiglietto(int codiceBiglietto)
        {
            ib = (from b in myDatabase.getDb().Biglietto
                  where b.codice_biglietto == codiceBiglietto
                  select new InfoBiglietto
                  {
                      nome = b.nome_intestatario,
                      cognome = b.cognome_intestatario,
                      cod_fiscale = b.Prenotazione.Passeggero.CF,
                      cod_volo = b.Prenotazione.Tariffario.numero_volo,
                      aereop_partenza = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_partenza,
                      aereop_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_arrivo,
                      ora_partenza = b.Prenotazione.Tariffario.Piano_di_volo.orario_partenza,
                      ora_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.orario_arrivo,
                      data_partenza = b.Prenotazione.Tariffario.Piano_di_volo.data_partenza,
                      data_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.data_arrivo,
                      spesa = b.Prenotazione.totale
                  }).First();
        }

        public void getPopulateDbTicket()
        {


            tariffario = (from t in myDatabase.getDb().Tariffarios
                          select t).ToList();
            classeVolo = (from cll in myDatabase.getDb().Classe
                          select cll).ToList();

            bagaglio = (from pbi in myDatabase.getDb().Prezzo_bagaglio_imbarcato
                        select pbi).ToList();

            comfort = (from c in myDatabase.getDb().Comfort
                       select c).ToList();

        }


        public void DatiAnagraficiBiglietto(TextBox tb)
        {
            throw new NotImplementedException();
        }

        public double getTotal(double kg, double quantitaBagagli, double numeroVolo, double confort, string classe)
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
                if (tariffario[a].numero_volo == (numeroVolo))
                {
                    tariffaSolaAndata = tariffario[a].tariffa_solo_andata;
                }
            }

            double classeScelta = 0;
            for (int cl = 0; cl < classeVolo.Count; cl++)
            {
                if (classeVolo[cl].descrizione.Equals(classe))
                {
                    classeScelta = classeVolo[cl].prezzo;
                }
            }
            return (prezzoBagaglio * quantitaBagagli) + (tariffaSolaAndata + confort) + (classeScelta);
        }


        public void firstTicket(List<String> l)
        {
            primoTicket = l;
        }

        public List<string> getFirstTicket()
        {
            return primoTicket;
        }

        public string getNome()
        {
            return ib.nome;
        }

        public string getCognome()
        {
            return ib.cognome;
        }

        public string getCF()
        {
            return ib.cod_fiscale;
        }

        public string getCodiceVolo()
        {
            return ib.cod_volo.ToString();
        }

        public string getAereoportoAndata()
        {
            return ib.aereop_partenza;
        }

        public string getAereoportoArrivo()
        {
            return ib.aereop_arrivo;
        }

        public string getOraPartenza()
        {
            return ib.ora_partenza.ToString();
        }

        public string getOraArrivo()
        {
            return ib.ora_arrivo.ToString();
        }

        public string getDataArrivo()
        {
            return ib.data_arrivo.ToString("dd/MM/yyyy");
        }

        public string getDataPartenza()
        {
            return ib.data_partenza.ToString("dd/MM/yyyy");
        }

        public string getSpesaTotale()
        {
            return ib.spesa.ToString();
        }
    }
}
