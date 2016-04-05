using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    interface Volo
    {
        bool getExistDestination(string destinazione);
        dynamic Cerca_volo(string partenza, string destinazione, DateTime data_partenza, DateTime data_ritorno);
    }
}
