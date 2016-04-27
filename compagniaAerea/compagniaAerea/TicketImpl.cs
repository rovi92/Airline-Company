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
    
        InfoBiglietto ib = new InfoBiglietto();
        int idTariffa = 0;
        int lastIdBiglietto = 0;
        int quantitaPersone = 0;
        int idPrenotazioneAndata = 0;
        int idPrenotazioneRitorno = 0;
        


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




        #region GET ticket e tutto ciò che è correlato al biglietto
        public double getTicketPrice(int idBiglietto)
        {
            return (from b in myDatabase.getDb().Biglietto
                    where b.codice_biglietto == idBiglietto
                    select b.Prenotazione.Tariffario.tariffa_solo_andata + b.Prenotazione.Tariffario.Classe.prezzo + b.Comfort_inclusi.Sum(c => c.Comfort.prezzo) + b.Babaglio_Imbarcato.Sum(bi => bi.Prezzo_bagaglio_imbarcato.prezzo)).First();
        }

        private double CalculatePrice(double price,int? sconto)
        {
            if(sconto == null)
            {
                return price;
            }
            else
            {
                return price - (price * Convert.ToDouble(sconto) / 100);
            }
        }


        public void updatePrenotationPrice(int idPrenotazione)
        {
            List<int> ticketsId = (from b in myDatabase.getDb().Biglietto
                                   where b.idPrenotazione == idPrenotazione
                                   select b.codice_biglietto).ToList();
            Double total = 0;
            foreach (int i in ticketsId)
            {

                total += getTicketPrice(i);

            }
            Prenotazione p = myDatabase.getDb().Prenotazione.First(pr => pr.idPrenotazione == idPrenotazione);
            p.totale = total;
            myDatabase.getDb().SubmitChanges();
        }
        public Double getPrenotationPrice(int idPrenotazione)
        {
            return myDatabase.getDb().Prenotazione.First(pr => pr.idPrenotazione == idPrenotazione).totale;
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

        public int getCurrentIdTariffa()
        {
            return this.idTariffa;
        }
        public int getIdTariffa(int numeroVolo, int numeroClasse)
        {

           
            this.idTariffa= myDatabase.getDb().Tariffario.First(t => t.idClasse == numeroClasse && t.numero_volo == numeroVolo).idTariffa;
            return this.idTariffa;
        }

        #endregion

        #region CREATE TICKET

        public void createBooking(string dataPrenotazione, int numeroPersone,double totale, int idTariffa,string tipoViaggio)
        {
            Prenotazione pr = new Prenotazione()
            {
                data_prenotazione = Convert.ToDateTime(dataPrenotazione).ToUniversalTime(),
                numero_persone = numeroPersone,
                totale = totale,
                idTariffa = idTariffa,
            };
            
            myDatabase.getDb().Prenotazione.InsertOnSubmit(pr);
            myDatabase.getDb().SubmitChanges();
            this.quantitaPersone = numeroPersone;
            if (tipoViaggio.Equals("Andata"))
            {
                idPrenotazioneAndata = pr.idPrenotazione;
            }
            else
            {
                idPrenotazioneRitorno = pr.idPrenotazione;
            }
        }

        public void insertRecordPagamento(string dataPagamento, string tipoPagamento, int idprenotazione)
        {
            Pagamento pa = new Pagamento()
            {
              
                data_pagamento = Convert.ToDateTime(dataPagamento),
                tipo_pagamento = tipoPagamento,
                idPrenotazione = idprenotazione
            };
            myDatabase.getDb().Pagamento.InsertOnSubmit(pa);
            myDatabase.getDb().SubmitChanges();
        }
      
         public void insertRecordTicket(int idPasseggero, int idPrenotazione)
        {
            Biglietto b = new Biglietto()
            {
                idPasseggero = idPasseggero,
                idPrenotazione = idPrenotazione
            };
            myDatabase.getDb().Biglietto.InsertOnSubmit(b);
            myDatabase.getDb().SubmitChanges();
            lastIdBiglietto = b.codice_biglietto;
        }
        
        public int getQuatitàPersone()
        {
            return this.quantitaPersone;
        }

        public int getIdPrenotazioneAndata()
        {
            return this.idPrenotazioneAndata;
        }

        public int getIdPrenotazioneRitorno()
        {
            return this.idPrenotazioneRitorno;
        }

        public void createFlightComfort(int idComfort)
        {
            Comfort_inclusi c = new Comfort_inclusi()
            {
                idComfort = idComfort,
              idBiglietto = this.lastIdBiglietto,

            };
            myDatabase.getDb().Comfort_inclusi.InsertOnSubmit(c);
            myDatabase.getDb().SubmitChanges();
        }

        public void createFlightLuggage(int range)
        {
            Babaglio_Imbarcato b = new Babaglio_Imbarcato()
            {
                range_pesi = range,
                codice_biglietto = this.lastIdBiglietto
            };
             myDatabase.getDb().Babaglio_Imbarcato.InsertOnSubmit(b);
            myDatabase.getDb().SubmitChanges();
        }

     

        public int getLastIdBiglietto()
        {
            return this.lastIdBiglietto;
        }

        public void setQuantitaPersone(int nPersone)
        {
            this.quantitaPersone = nPersone;
        }
      
        #endregion
    }
}
