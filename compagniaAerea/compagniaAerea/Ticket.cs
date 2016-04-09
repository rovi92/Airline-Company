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
        String getNome(int codiceBiglietto);// prelievo del nome dell'intestatario del biglio
        String getCognome(int codiceBiglietto);//prelievo cognome intestatario biglietto
        String getCF(int codiceBiglietto);//prelievo codice fiscale intestatario biglietto
        Int32 getCodiceVolo(int codiceBiglietto);// codice del volo
        String getAereoportoAndata(int codiceBiglietto);
        String getAereoportoArrivo(int codiceBiglietto);
        TimeSpan getOraPartenza(int codiceBiglietto);
        TimeSpan getOraArrivo(int codiceBiglietto);
        DateTime getDataArrivo(int codiceBiglietto);
        DateTime getDataPartenza(int codiceBiglietto);
        Double getSpesaTotale(int codiceBiglietto);//prelievo delle varie spese e somma delle stesse per ottenere una spesa totale
       
    }
}
