using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    interface DipendenteVolo
    {
        void Aggiungi_tratta(String aeroporto_partenza, String aeroporto, int gate_partenza, int gate_arrivo, DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo);
        void Aggiungi_pianodivolo(DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo, Boolean cancellato);
        void UpdatePianodivolo();
        Boolean checkPianodivoloExist(DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo);
    }
}
