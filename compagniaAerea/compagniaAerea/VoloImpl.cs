using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace compagniaAerea
{
    class VoloImpl : Volo
    {   
          
        DatabaseManager myDatabase;
      List<Tratta> trattaLocale = new List<Tratta>();

        public VoloImpl() {

            myDatabase = DatabaseManager.Instance;//al
            
          
            
        }
        //caricamento di database in locale 
        public void executeTratta()
        {
            trattaLocale = (from t in myDatabase.getDb().Tratta
                           // where t.aeroporto_partenza.Equals = 
                            select t).ToList();

        }

        //metodo per controllare se esiste un una certa stringa nel database
        public Boolean getExistDestination(string destinazione)
        {
            Boolean flag = false;
            foreach (Tratta t in trattaLocale){
                if (t.aeroporto_partenza.Equals(destinazione))
                {
                    flag = true; 
                }
            }
            if (flag.Equals(false))
            {
                MessageBox.Show("non abbiamo voli in partenza da questa località");
            }
            return flag;
            
        }

        public Boolean getExistArrive(string arrivo)
        {

            Boolean flag = false;
            foreach (Tratta t in trattaLocale)
            {
                if (t.aeroporto_arrivo.Equals(arrivo))
                {
                    flag = true;
                }
            }
            if (flag.Equals(false))
            {
                MessageBox.Show("non abbiamo voli con arrivo in questa località");
            }
            return flag;
        }

        public Boolean getExistTimeDestination(string data, string tipo)
        {
            Boolean flag = false;
            switch(tipo)
            {
                case "dataPartenza":
                    foreach(Tratta t in trattaLocale)
                    {

                        if (t.data_partenza.ToString("yyyy-MM-dd").Equals(data))
                        {
                            return flag = true;
                        }
                    }
                    MessageBox.Show("nessuna partenza in questa data");
                    return flag;
                case "dataRitorno":
                    foreach (Tratta t in trattaLocale)
                    {
                        if (t.data_partenza.ToString("yyyy-MM-dd").Equals(data))
                        {
                           return flag = true;
                        }
                    }
                    MessageBox.Show("nessuna partenza in questa data");
                    return flag;
                default:
                    return flag;
            }

        }
        //Cerca volo
        #region cerca volo MARALDI
        /* public dynamic Cerca_volo(string partenza, string destinazione, DateTime data_partenza, DateTime data_ritorno)
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

          }*/

        #endregion

    }
}
