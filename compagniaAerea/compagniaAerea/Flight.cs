using System;
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
        Boolean checkFlightSeats(int idClasse,int idVolo, int postiInPrenotamento);
        void updateFlightLegs();

        void setFlightClass(string className,int idClasse);

        List<FlightImpl.FlightInfo> getCustomFlight(string partenza, string arrivo, string data);
        List<FlightImpl.FlightInfo> getFlights();
        string getFlightClassName();
        int getFlightClassId();
        void CancelFlight(int numero_volo);
        string getAirportName(string città);
        int CountFlightLegs(int idVolo);


    }


}
