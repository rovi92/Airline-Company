﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace compagniaAerea
{
    interface Flight
    {
        Boolean checkDeparture(string departure);
        Boolean checkArrival(string arrival);
        Boolean checkDateFlight(string date);

        void updateFlightLegs();

        void setFlightClass(string className,int idClasse);

        List<FlightImpl.FlightInfo> getCustomFlight(string partenza, string arrivo, string data);
        string getFlightClassName();
        int getFlyClassId();
        string getAirportName(string città);
        
       
    }


}
