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
            var query = (from b in myDatabase.getDb().Biglietto
                             //where b.codice_biglietto.Equals(codiceBiglietto)
                         select new
                         {
                             cod_biglietto = b.codice_biglietto,//indice 0
                             nome_intestatario = b.nome_intestatario,//indeice 1
                             cognome_intestatario = b.cognome_intestatario,//2
                             cod_fiscale = b.Prenotazione.Passeggero.CF,//3
                             cod_volo = b.Prenotazione.Tariffario.numero_volo,//4
                             aerep_andata = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_partenza,//5
                             aerep_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.Tratta.First(t => t.numero_volo.Equals(b.Prenotazione.Tariffario.numero_volo)).aeroporto_arrivo,//6
                             ora_partenza = b.Prenotazione.Tariffario.Piano_di_volo.orario_partenza,//7
                             ora_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.orario_arrivo,//8
                             data_partenza = b.Prenotazione.Tariffario.Piano_di_volo.data_partenza,//9
                             data_arrivo = b.Prenotazione.Tariffario.Piano_di_volo.data_arrivo,//10
                             prezzo = (b.Babaglio_Imbarcato.Where(bi => bi.peso <= bi.Prezzo_bagaglio_imbarcato.range_pesi).First().Prezzo_bagaglio_imbarcato.prezzo) + ((
                                                                                                                                                        b.Prenotazione.Tariffario.tariffa_solo_andata +
                                                                                                                                                        b.Prenotazione.Tariffario.Comfort_inclusi.Where(ci => ci.idComfort <= ci.Comfort.idComfort).First().Comfort.prezzo) *
                                                                                                                                                        b.Prenotazione.Tariffario.Classe.prezzo)//11
                         }).ToList();
            biglietti = new ListCollectionView(query);
        }

        #region funzione visione biglietto

        public String getNome(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            string nome = "";
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))
                //controllo corrispondenza tra nome cognome e codice inseriti da utente e quelli presenti nel DB
                {
                    nome = b.nome_intestatario;
                }
                break;
            }
            return nome;
        }
        public String getCognome(int codiceBiglietto, string nome_intestatario, string cognome_intestatario)
        {
            string cognome = "";
            foreach (Biglietto b in biglietti)
            {
                if (b.codice_biglietto.Equals(codiceBiglietto) && b.nome_intestatario.Equals(nome_intestatario) && b.cognome_intestatario.Equals(cognome_intestatario))

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

        #endregion

        public void DatiAnagraficiBiglietto(TextBox tb)
        {
            throw new NotImplementedException();
        }
    }
}
