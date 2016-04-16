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
        void setQuantitàPersone(int persone);
        
        void getPopulateDbTicket();//prelievo delle varie spese e somma delle stesse per ottenere una spesa totale
        Double getTotal(double kg, double quantitaBagagli, double numeroVolo,double cofort,int classe);
        int getIdTariffa(int numeroVolo,int numeroClasse);
        void createBooking(string dataPrenotazione,int numeroPersone,double totale,int idPasseggero,int idTariffa);
        int getIdPrenotaione();
        void insertRecordPagamento(string dataPagamento,string tipoPagamento,int idprenotazione);
        void insertRecordTiket(string nomeIntestatario,string cognomeIntestatario,int idPrenotazione);
        
        void firstTicket(List<String> l);
        List<String> getFirstTicket();
    }
}
