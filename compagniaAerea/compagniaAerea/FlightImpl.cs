using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace compagniaAerea
{
    class FlightImpl : Flight
    {

        DatabaseManager myDatabase;
        List<FlightInfo> voli = new List<FlightInfo>();
        string nomeClasse = "";
        int idClasse = 0;

        public FlightImpl()
        {

            myDatabase = DatabaseManager.Instance;//al


        }
        public class FlightInfo

        {

            public string partenza { get; set; }//1

            public string arrivo { get; set; }//2

            public DateTime dataPartenza { get; set; }//nella g non c'è

            public DateTime dataArrivo { get; set; }//nella g non c'è

            public TimeSpan orarioPartenza { get; set; }//6

            public TimeSpan orarioArrivo { get; set; }//7

            public int codiceVolo { get; set; }//0

            public double costoEconomy { get; set; }//3

            public double costoBuisness { get; set; }//4

            public double costoFirst { get; set; }//5

            public Boolean cancellato { get; set; }

            }

        //caricamento di database in locale 
        public void updateFlightLegs()
        {
            voli.Clear();
            voli.AddRange((from p in myDatabase.getDb().Piano_di_volo
                           select new FlightInfo
                           {
                               partenza = p.Aeroporto.città,
                               arrivo = p.Aeroporto1.città,
                               dataPartenza = p.data_partenza,
                               dataArrivo = p.data_arrivo,
                               orarioPartenza = p.orario_partenza,
                               orarioArrivo = p.orario_arrivo,
                               codiceVolo = p.numero_volo,
                               costoEconomy = p.Tariffario.First(t => t.idClasse == 1).tariffa_solo_andata,
                               costoBuisness = p.Tariffario.First(t => t.idClasse == 2).tariffa_solo_andata,
                               costoFirst = p.Tariffario.First(t => t.idClasse == 3).tariffa_solo_andata,
                               cancellato = (Boolean)p.cancellato
                           }).ToList());


        }

        public List<FlightInfo> getFlights()
        {
            updateFlightLegs();
            return voli;
        }

        public List<FlightInfo> getCustomFlight(string partenza, string arrivo, string data)
        {
            return (from v in voli
                    where v.partenza == partenza && v.arrivo == arrivo && v.dataPartenza.ToString("yyyy-MM-dd") == data && v.cancellato == false
                    select v).ToList();
            
        }
        //metodo per controllare se esiste un una certa stringa nel database

        public Boolean checkDeparture(string departure)
        {
            return (from v in voli
                    where v.partenza == departure
                    select v).Count() > 0 ? true : false;
           

        }

        public Boolean checkArrival(string arrival)
        {

            return (from v in voli
                    where v.arrivo == arrival
                    select v).Count() > 0 ? true : false;
        }

        public Boolean checkDateFlight(string date)
        {
            return (from v in voli
                    where v.dataPartenza.ToString("yyyy-MM-dd") == date
                    select v).Count() > 0 ? true : false;
          
        }

        public void setFlightClass(String className)
        {
            this.nomeClasse = className;
        }

        public String getFlightClassName()
        {
            return this.nomeClasse;
        }

        public int getFlyClassId()
        {
            return this.idClasse;
        }

        public void CancelFlight(int numero_volo)
        {
            Piano_di_volo p = myDatabase.getDb().Piano_di_volo.First(pv => pv.numero_volo == numero_volo);
            p.cancellato = true;
            myDatabase.getDb().SubmitChanges();
        }

        public string getAirportName(string città)
        {
            return myDatabase.getDb().Aeroporto.First(a => a.città.ToString() == città).nome;
           
        }
    }
}
