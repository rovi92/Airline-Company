using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace compagniaAerea
{
    class VoloImpl : Volo
    {

        DatabaseManager myDatabase;
        List<Tratta> trattaLocale = new List<Tratta>();
        string className = "";
        int idClasse = 0;

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

            public string codiceVolo { get; set; }

            public string costoViaggio { get; set; }



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

                if (nandata.Equals(trattaLocale[i].Aeroporto.città) && nRitorno.Equals(trattaLocale[i].Aeroporto1.città) && data.Equals(trattaLocale[i].data_partenza.ToString("yyyy-MM-dd")))
                {
                    int posti = 0;
                    int postiMax = 0;
                    switch (this.className)
                    {
                        case "Economy":
                            posti = trattaLocale[i].posti_economy;
                            postiMax = trattaLocale[i].Aereo.capacità_economy;
                            this.idClasse = 1;
                            break;
                        case "Buisness":
                            posti = trattaLocale[i].posti_buisness;
                            postiMax = trattaLocale[i].Aereo.capacità_buisness;
                            this.idClasse = 2;
                            break;
                        case "First":
                            posti = trattaLocale[i].posti_first;
                            postiMax = trattaLocale[i].Aereo.capacità_first;
                            this.idClasse = 3;

                            break;
                        default:
                            this.idClasse = -1;
                            break;
                    }
                    if (posti < postiMax)
                    {
                        flyList.Add(new InfoViaggio()
                        {
                            partenza = trattaLocale[i].Aeroporto.città,
                            arrivo = trattaLocale[i].Aeroporto1.città,
                            costoViaggio = trattaLocale[i].Piano_di_volo.Tariffario.First(t => t.numero_volo == trattaLocale[i].Piano_di_volo.numero_volo).tariffa_solo_andata.ToString(),
                            dataPartenza = trattaLocale[i].data_partenza.ToString("yyyy-MM-dd"),
                            orarioPartenza = trattaLocale[i].orario_partenza.ToString(),
                            orarioArrivo = trattaLocale[i].orario_arrivo.ToString(),
                            codiceVolo = trattaLocale[i].numero_volo.ToString(),



                        });
                    }
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
                if (t.Aeroporto.città.Equals(destinazione))
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
                if (t.Aeroporto1.città.Equals(arrivo))
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
                    MessageBox.Show("Nessuna partenza in questa data");
                    return flag;
                case "dataRitorno":
                    foreach (Tratta t in trattaLocale)
                    {

                        if (t.data_partenza.ToString("yyyy-MM-dd").Equals(data))
                        {
                            return flag = true;
                        }
                    }
                    MessageBox.Show("Nessuna partenza in questa data");
                    return flag;
                default:
                    return flag;
            }

        }

        public void setClass(String className)
        {
            this.className = className;
        }

        public String getClassName()
        {
            return this.className;
        }

        public int getClassId()
        {
            return this.idClasse;
        }

        public List<String> getValueGrid(DataGrid dg)
        {
            //Convert.ToInt32((dgOrdini.SelectedCells[1].Column.GetCellContent(dgOrdini.SelectedItem) as TextBlock).Text);
            List<String> tmp = new List<String>();
            for (int i = 0; i < dg.SelectedCells.Count; i++)
            {
                tmp.Add((dg.SelectedCells[i].Column.GetCellContent(dg.SelectedItem) as TextBlock).Text);
            }
            return tmp;
       
    }

        public string getNameAirport(string città)
        {
            string c = "";
            for (int i = 0; i < trattaLocale.Count(); i++)
            {
                if (trattaLocale[i].Aeroporto.città.Equals(città))
                {
                    c = trattaLocale[i].Aeroporto.nome;
                }

            }
            return c;
        }
    }
}
