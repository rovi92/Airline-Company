using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    interface Dipendente
    {
        void getDipendente(string nome_dipendete, string cognome_dipendente/*, string psw*/);
        String getNome();
        String getCognome();
        String getPsw();
    }
}
