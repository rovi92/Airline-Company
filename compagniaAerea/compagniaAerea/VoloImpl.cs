using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    class VoloImpl : Volo
    {
        DatabaseManager myDatabase;
        public VoloImpl(){
            myDatabase = DatabaseManager.Instance;//al
        }
        public bool getExistDestination(string destinazione)
        {
            throw new NotImplementedException();
        }
    }
}
