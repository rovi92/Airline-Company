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
        public VoloImpl() {
            myDatabase = DatabaseManager.Instance;//al
        }
        public bool getExistDestination(string destinazione)
        {
            throw new NotImplementedException();
        }

        //Cerca volo
        #region cerca volo
        public dynamic Cerca_volo(string partenza, string destinazione, DateTime data_partenza, DateTime data_ritorno)
        {
            var cerca_volo = (from tratta in myDatabase.getDb().Tratta
                             join aeroportop in myDatabase.getDb().Aeroporto on tratta.aeroporto_partenza equals aeroportop.nome
                             join aeroportoa in myDatabase.getDb().Aeroporto on tratta.aeroporto_arrivo equals aeroportoa.nome
                             where tratta.gate_partenza == 2
                             select new
             {
                 tratta.data_partenza,
                 tratta.data_arrivo,
                 aeroportop.nome,
                 aeroportoa.telefono
         

             });

            return cerca_volo;
        }
        #endregion

    }
}
