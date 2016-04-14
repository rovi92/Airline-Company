using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace compagniaAerea
{
    interface Volo
    {
        Boolean getExistDestination(string destinazione);
        Boolean getExistArrive(string arrivo);
        Boolean getExistTimeDestination(string data, string tipo);
        List<VoloImpl.InfoViaggio> getFly(string nandata, string nRitorno, string data);
        void setClass(string className);
        String getClassName();
        int getClassId();

        List<String> getValueGrid(DataGrid dg);
        string getNameAirport(string città);
        
       
        void executeTratta();//aggiornamento entità locale tratta per la lettura dei dati
                             //  dynamic Cerca_volo(string partenza, string destinazione, DateTime data_partenza, DateTime data_ritorno);
    }


}
