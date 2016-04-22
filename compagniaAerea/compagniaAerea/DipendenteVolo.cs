using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    interface DipendenteVolo
    {
        void Aggiungi_pianodivolo(String città_partenza, String città_arrivo, DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo, Boolean cancellato, double tariffa);
        void Aggiungi_tratta(String città_partenza, String città_arrivo, int gate_partenza, int gate_arrivo, DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo, String[] aereo, int numero_volo);
        void UpdatePianodivolo();
        Boolean checkPianodivoloExist(String città_partenza, String città_arrivo, String data_partenza, String data_arrivo, String orario_partenza, String orario_arrivo);
        int getNumeroVolo(String città_partenza, String città_arrivo, String data_partenza, String data_arrivo, String orario_partenza, String orario_arrivo);
        void CreateFlightFare(int numero_volo, double tariffa);
    }
}
