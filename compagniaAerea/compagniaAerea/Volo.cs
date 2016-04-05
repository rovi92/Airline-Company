using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    interface Volo
    {
        Boolean getExistDestination(string destinazione);
        Boolean getExistArrive(string arrivo);
        Boolean getExistTimeDestination(string data,string tipo);
        void executeTratta();//aggiornamento entità locale tratta per la lettura dei dati
      //  dynamic Cerca_volo(string partenza, string destinazione, DateTime data_partenza, DateTime data_ritorno);
    }
}
