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

            public Boolean? cancellato { get; set; }//nella g non c'è

            public int? idPromozione { get; set; }

        }
        public class FlightLegsInfo
        {
            public TimeSpan orarioPartenza { get; set; }
            public DateTime dataPartenza { get; set; }
            public int gatePartenza { get; set; }
            public string partenza { get; set; }
            public string arrivo { get; set; }
        }
        //caricamento di database in locale 
        public void updateFlightLegs()
        {
            voli.Clear();
            /*voli.AddRange((from p in myDatabase.getDb().Piano_di_volo
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
                               cancellato = p.cancellato,
                               idPromozione = p.idPromozione
                           }).ToList());*/

        }

        public List<FlightInfo> getFlights()
        {
            updateFlightLegs();
            return voli;
        }

        public List<FlightLegsInfo> getFlightsLegs()
        {
            return (from t in myDatabase.getDb().Tratta
                    select new FlightLegsInfo
                    {
                        orarioPartenza = t.orario_partenza,
                        dataPartenza = t.data_partenza,
                        gatePartenza = t.gate_partenza,
                        partenza = t.Aeroporto.città,
                        arrivo = t.Aeroporto1.città
                    }).ToList();
        }

        public List<FlightInfo> getCustomFlight(string partenza, string arrivo, string data)
        {
            return (from v in voli
                    where v.partenza == partenza && v.arrivo == arrivo && v.dataPartenza.ToString("yyyy-MM-dd") == data && v.cancellato == false
                    select v).ToList();

        }

        /*QUESTA E' LA PARTE CHE ABBIAMO APPENA AGGIUNTO*/
        public List<FlightInfo> getLastMinute()
        {
            return (from v in voli
                    where v.dataPartenza == DateTime.Today.AddDays(1)
                    select v).ToList();
        }

        public List<FlightInfo> getXmasFlights()
        {
            return (from v in voli
                    where v.dataPartenza.ToString("yyyy-MM-dd") == DateTime.Today.Year.ToString() + "-12-25"
                    select v).ToList();
        }

        public List<FlightInfo> getSummerBankHolidayFlights()
        {
            return (from v in voli
                    where v.dataPartenza.ToString("yyyy-MM-dd") == DateTime.Today.Year.ToString() + "-08-15"
                    select v).ToList();
        }
        /*FINE PARTE APPENA AGGIUNTA*/

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

        public void setFlightClass(String className, int idClasse)
        {
            this.nomeClasse = className;
            this.idClasse = idClasse;
        }

        public String getFlightClassName()
        {
            return this.nomeClasse;
        }

        public int getFlightClassId()
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

        public Boolean checkFlightSeats(int idVolo, int idClasse, int postiInPrenotamento)
        {
            //prossima modifica action c# code provider

            switch (idClasse)
            {
                case 1:
                    return (from p in myDatabase.getDb().Piano_di_volo
                            where p.numero_volo == idVolo
                            select p.Tratta).Count() > 1 ? (from p in myDatabase.getDb().Piano_di_volo
                                                            where p.numero_volo == idVolo
                                                            select ((p.Tratta.First().Aereo.capacità_economy - (p.Tratta.First().posti_economy + postiInPrenotamento)) > 0) && ((p.Tratta.Last().Aereo.capacità_economy - (p.Tratta.Last().posti_economy + postiInPrenotamento)) > 0)).First() : (from p in myDatabase.getDb().Piano_di_volo
                                                                                                                                                                                                                                                                                                  where p.numero_volo == idVolo
                                                                                                                                                                                                                                                                                                  select ((p.Tratta.First().Aereo.capacità_economy - (p.Tratta.First().posti_economy + postiInPrenotamento)) > 0)).First();


                case 2:
                    return (from p in myDatabase.getDb().Piano_di_volo
                            where p.numero_volo == idVolo
                            select p.Tratta).Count() > 1 ? (from p in myDatabase.getDb().Piano_di_volo
                                                            where p.numero_volo == idVolo
                                                            select ((p.Tratta.First().Aereo.capacità_buisness - (p.Tratta.First().posti_buisness + postiInPrenotamento)) > 0) && ((p.Tratta.Last().Aereo.capacità_buisness - (p.Tratta.Last().posti_buisness + postiInPrenotamento)) > 0)).First() : (from p in myDatabase.getDb().Piano_di_volo
                                                                                                                                                                                                                                                                                                      where p.numero_volo == idVolo
                                                                                                                                                                                                                                                                                                      select ((p.Tratta.First().Aereo.capacità_buisness - (p.Tratta.First().posti_buisness + postiInPrenotamento)) > 0)).First();


                case 3:
                    return (from p in myDatabase.getDb().Piano_di_volo
                            where p.numero_volo == idVolo
                            select p.Tratta).Count() > 1 ? (from p in myDatabase.getDb().Piano_di_volo
                                                            where p.numero_volo == idVolo
                                                            select ((p.Tratta.First().Aereo.capacità_first - (p.Tratta.First().posti_first + postiInPrenotamento)) > 0) && ((p.Tratta.Last().Aereo.capacità_first - (p.Tratta.Last().posti_first + postiInPrenotamento)) > 0)).First() : (from p in myDatabase.getDb().Piano_di_volo
                                                                                                                                                                                                                                                                                          where p.numero_volo == idVolo
                                                                                                                                                                                                                                                                                          select ((p.Tratta.First().Aereo.capacità_first - (p.Tratta.First().posti_first + postiInPrenotamento)) > 0)).First();


                default:
                    return false;
            }

        }
        public int CountFlightLegs(int idVolo)
        {
            return (from p in myDatabase.getDb().Piano_di_volo
                    where p.numero_volo == idVolo
                    select p.Tratta).Count();
        }

        public void updateFlightSeats(int idVolo, int idClasse, int postiPrenotati)
        {
           
            List<int> posti = new List<int>();
            posti.AddRange(from t in myDatabase.getDb().Tratta
                           where t.numero_volo == idVolo
                           select idClasse == 1 ? t.posti_economy : idClasse == 2 ? t.posti_buisness : t.posti_first);

            if (posti.Count() > 1)
            {
                posti[0] += postiPrenotati;
                posti[1] += postiPrenotati;

                List<Tratta> t0 = myDatabase.getDb().Tratta.Where(t => t.numero_volo == idVolo).ToList();
                
           
                
                if (idClasse == 1)
                {
                    t0[0].posti_economy = posti[0];
                    t0[1].posti_economy = posti[1];
                }
                else
                {
                    if (idClasse == 2)
                    {
                        t0[0].posti_buisness = posti[0];
                        t0[1].posti_economy = posti[1];
                    }
                    else
                    {
                        t0[0].posti_first = posti[0];
                        t0[1].posti_first = posti[1];
                    }
                }
                myDatabase.getDb().SubmitChanges();
            }
            else
            {
                posti[0] += postiPrenotati;
                Tratta t0 = myDatabase.getDb().Tratta.First(t => t.numero_volo == idVolo);

                if (idClasse == 1)
                {
                    t0.posti_economy = posti[0];
                }
                else
                {
                    if (idClasse == 2)
                    {
                        t0.posti_buisness = posti[0];
                    }
                    else
                    {
                        t0.posti_first = posti[0];
                    }
                }
                myDatabase.getDb().SubmitChanges();

            }
        }
    }
}
