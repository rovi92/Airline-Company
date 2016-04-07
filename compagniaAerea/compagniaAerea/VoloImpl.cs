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


        public VoloImpl()
        {

            myDatabase = DatabaseManager.Instance;//al


        }
        public class InfoViaggio

        {

            public string partenza { get; set; }

            public string arrivo { get; set; }

            public string dataPartenza { get; set; }

            public string orarioPartenza { get; set; }

            public string orarioArrivo { get; set; }
        }

        //caricamento di database in locale 
        public void executeTratta()
        {
            trattaLocale = (from t in myDatabase.getDb().Tratta
                             
                            select t).ToList();
        


        }
        public List<InfoViaggio> getFly(string nandata, string nRitorno, string data)
        {
            List<InfoViaggio> flyList = new List<InfoViaggio>();

            for (int i = 0; i < trattaLocale.Count; i++)
            {
                if (nandata.Equals(trattaLocale[i].aeroporto_partenza) && nRitorno.Equals(trattaLocale[i].aeroporto_arrivo) && data.Equals(trattaLocale[i].data_partenza.ToString("yyyy-MM-dd")))
                {
                    flyList.Add(new InfoViaggio()
                    {
                        partenza = trattaLocale[i].aeroporto_partenza,
                        arrivo = trattaLocale[i].aeroporto_arrivo,
                        dataPartenza = trattaLocale[i].data_partenza.ToString("yyyy-MM-dd"),
                        orarioPartenza = trattaLocale[i].orario_partenza.ToString(),
                        orarioArrivo = trattaLocale[i].orario_arrivo.ToString()
                    });
                }
            }
            return flyList;
        }
        //metodo per controllare se esiste un una certa stringa nel database
        public Boolean getExistDestination(string destinazione)
        {
            Boolean flag = false;
            foreach (Tratta t in trattaLocale)
            {
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
            switch (tipo)
            {
                case "dataPartenza":
                    foreach (Tratta t in trattaLocale)
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

        public void setClass(string classeVolo)
        {

        }
    }
}
