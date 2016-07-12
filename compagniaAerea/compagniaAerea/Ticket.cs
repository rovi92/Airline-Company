using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace compagniaAerea
{
    interface Ticket
    {
        void getBiglietto(int codiceBiglietto);
     
        String getNome();// prelievo del nome dell'intestatario del biglio
        String getCognome();//prelievo cognome intestatario biglietto
        String getCF();//prelievo codice fiscale intestatario biglietto
        String getCodiceVolo();// codice del volo
        String getAereoportoAndata();
        String getAereoportoArrivo();
        String getOraPartenza();
        String getOraArrivo();
        String getDataArrivo();
        String getDataPartenza();
        String getSpesaTotale();

        int getQuatitàPersone();
       
        
        Double getTicketPrice(int idBiglietto);
       
        int getIdTariffa(int numeroVolo,int numeroClasse);
        void createBooking(string dataPrenotazione,int numeroPersone,double totale,int idTariffa,string tipoViaggio);
        void createFlightComfort(int idComfort);
        void createFlightLuggage(int range);
        int getLastIdBiglietto();
        void updatePrenotationPrice(int idPrenotazione);
        Double getPrenotationPrice(int idPrenotazione);

        int getCurrentIdTariffa();
        int getIdPrenotazioneAndata();
        int getIdPrenotazioneRitorno();
        void insertRecordPagamento(string dataPagamento,string tipoPagamento,int idprenotazione);
        void insertRecordTicket(int idPasseggero,int idPrenotazione);
       // void firstTicket(List<String> l);
        //List<String> getFirstTicket();
        void setQuantitaPersone(int nPersone);
       
    }
}
