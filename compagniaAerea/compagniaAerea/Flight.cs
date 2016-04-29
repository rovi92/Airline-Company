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
        Boolean checkDateFlight(DateTime? date);
        Boolean checkFlightSeats(int idClasse,int idVolo, int postiInPrenotamento);
        void UpdateFlights();

        void setFlightClass(string className,int idClasse);
        List<FlightImpl.FlightInfo> getCustomFlight(string partenza, string arrivo, DateTime? data);
        List<FlightImpl.FlightInfo> getFlights();
        List<FlightImpl.FlightInfo> getLastMinute();
        List<FlightImpl.FlightInfo> getXmasFlights();
        List<FlightImpl.FlightInfo> getSummerBankHolidayFlights();
        void addDiscount(int numero_volo, int idPromozione);
        string getFlightClassName();
        int getFlightClassId();
        void CancelFlight(int numero_volo);
        string getAirportName(string città);
        int CountFlightLegs(int idVolo);
        void updateFlightSeats(int idVolo, int idClasse, int postiPrenotati);
        string getFlightLegDeparture(int idVolo);
        string getFlightLegDepartureTime(int idVolo);
        string getFlightLegArrivalTime(int idVolo);
        List<FlightImpl.FlightLegsInfo> getFlightsLegs();


    }


}
