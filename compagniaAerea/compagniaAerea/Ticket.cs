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
        String getNome(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);// prelievo del nome dell'intestatario del biglio
        String getCognome(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);//prelievo cognome intestatario biglietto
        String getCF(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);//prelievo codice fiscale intestatario biglietto
        Int32 getCodiceVolo(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);// codice del volo
        String getAereoportoAndata(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);
        String getAereoportoArrivo(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);
        TimeSpan getOraPartenza(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);
        TimeSpan getOraArrivo(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);
        DateTime getDataArrivo(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);
        DateTime getDataPartenza(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);
        Double getSpesaTotale(int codiceBiglietto, string nome_intestatario, string cognome_intestatario);//prelievo delle varie spese e somma delle stesse per ottenere una spesa totale
       
    }
}
