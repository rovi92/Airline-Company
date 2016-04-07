using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{

    class TicketImpl : Ticket
    {
        DatabaseManager myDatabase;
        public TicketImpl()
        {
            myDatabase = DatabaseManager.Instance;
        }
        public String getNome(int codiceBiglietto) {
           return (from b in myDatabase.getDb().Biglietto
             where codiceBiglietto.Equals(b.codice_biglietto)
             select b.nome_intestatario).ToString();
                            
        }
       public String getCognome(int codiceBiglietto) {
            return (from b in myDatabase.getDb().Biglietto
                    where codiceBiglietto.Equals(b.codice_biglietto)
                    select b.cognome_intestatario).ToString();
        }
       public String getCF(int codiceBiglietto) {
            return "";
        }
       public Int32 getCodiceVolo(int codiceBiglietto) {
            return 1;
        }
       public String getAereoportoAndata(int codiceBiglietto) {
            return "";
        }
       public String getAereoportoArrivo(int codiceBiglietto) {
            return "";
        }
       public TimeSpan getOraPartenza(int codiceBiglietto) {
            TimeSpan a = new TimeSpan (00,00,00,00);//giorni, ore , minuti, secondi
            return a;
        }
       public DateTime getOrataPartenza(int codiceBiglietto) {
            DateTime a = new DateTime(2016, 1, 1);
            return a;
        }
       public Double getSpesaTotale(int codiceBiglietto) {
            return 1;
        }
    }
}
