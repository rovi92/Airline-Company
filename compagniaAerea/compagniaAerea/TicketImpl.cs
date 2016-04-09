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
        ListCollectionView biglietti;
      

        public TicketImpl()
        {
            myDatabase = DatabaseManager.Instance;
        }

        public void getBiglitti(int codiceBiglietto)
        {
          var  query = (from b in myDatabase.getDb().Biglietto
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
                             prezzo_bagaglio = b.Babaglio_Imbarcato.Where(bi => bi.peso <= bi.Prezzo_bagaglio_imbarcato.range_pesi).First().Prezzo_bagaglio_imbarcato.prezzo +
                                                                                                                                    b.Prenotazione.Tariffario.tariffa_solo_andata
                         }).ToList();
            biglietti = new ListCollectionView(query);
        }
        
        public String getNome(int codiceBiglietto)
        {
            string nome = "";
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
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
        public String getCF(int codiceBiglietto)
        {
            string CF="";
           
                foreach (Biglietto b in biglietti)
                {
                    if (b.codice_biglietto.Equals(codiceBiglietto))
                    {
                        CF = b.Prenotazione.Passeggero.CF;
                    }
                    break;
                }
                return CF;
            }
        public Int32 getCodiceVolo(int codiceBiglietto)
        {
            int cod_volo=0;
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
                {
                    cod_volo = b.Prenotazione.Tariffario.numero_volo;
                }
                break;
            }
            return cod_volo;
        }
        public String getAereoportoAndata(int codiceBiglietto)
        {
            string andata ="";
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
                {
                    andata = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_partenza;
                }
                break;
            }
            return andata;
        }
        public String getAereoportoArrivo(int codiceBiglietto)
        { string ritorno = "";
                foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
                {
                    ritorno = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_arrivo;
                }
                break;
            }
            return ritorno;
        }
        public TimeSpan getOraPartenza(int codiceBiglietto)
        {
            TimeSpan ora = new TimeSpan(00, 00, 00, 00);//giorni, ore , minuti, secondi
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
                {
                    ora = b.Prenotazione.Tariffario.Piano_di_volo.orario_partenza;
                }
                break;
            }
           
            return ora;
        }
        public TimeSpan getOraArrivo (int codiceBiglietto)
        {
            TimeSpan ora = new TimeSpan(00, 00, 00, 00);//giorni, ore , minuti, secondi
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
                {
                    ora = b.Prenotazione.Tariffario.Piano_di_volo.orario_arrivo;
                }
                break;
            }
                            
            return ora;
        }
        public DateTime getDataPartenza(int codiceBiglietto)
        {
            DateTime data = new DateTime(2016, 1, 1);
           foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
                {
                    data = b.Prenotazione.Tariffario.Piano_di_volo.data_partenza;
        }
                break;
        }
            return data;
        }
        public DateTime getDataArrivo(int codiceBiglietto)
        {
            DateTime data = new DateTime(2016, 1, 1);
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
                {
                    data = b.Prenotazione.Tariffario.Piano_di_volo.data_arrivo;
        }
                break;
        }
            return data;
        }
        public Double getSpesaTotale(int codiceBiglietto)
        {
            double spesa = 0;
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto))
                {
                    spesa = b.Babaglio_Imbarcato.Where(bi => bi.peso <= bi.Prezzo_bagaglio_imbarcato.range_pesi).First().Prezzo_bagaglio_imbarcato.prezzo +
                                                                                                              b.Prenotazione.Tariffario.tariffa_solo_andata;
        }
                break;
        }
            return spesa;
        }

        public void DatiAnagraficiBiglietto(TextBox tb)
        {
            throw new NotImplementedException();
        }
    }
}
