using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusBooking.Shared.DTO;
using BusBooking.Data.DAL;

namespace BusBooking.Business.Authenticate
{
    public class Home : IHome
    {
        private readonly IReadData readObj;
        public Home(IReadData readObj)
        {
            this.readObj = readObj;
        }

        public List<BusDTO> GetSearchResults(string Source, string Destination, string Date, string Time, string TimeType)
        {
            var buses = readObj.GetBuses().Where(x => x.Source.Equals(Source, StringComparison.CurrentCultureIgnoreCase) && x.Destination.Equals(Destination, StringComparison.CurrentCultureIgnoreCase) && x.Status == 1).ToList();
                        
            if(Time != null && Time !="")
            {
                buses = buses.Where(x => TimeType.Equals("arrivaltime", StringComparison.CurrentCultureIgnoreCase) ? x.ArrivalTime.Equals(Time) : x.DepartureTime.Equals(Time)).ToList();
            }
            
            var searchReault = new List<BusDTO>();

            foreach(var bus in buses)
            {
                var bookings = readObj.GetBookings();

                var filteredBookings = bookings.Where(x => x.BusId == bus.BusId && x.BookingDate.Equals(Date) && x.Status==1).Select(x=>x.NoOfPassengers).ToList();
                
                var bookedSeats = filteredBookings.Sum();

                var availableSeats = bus.MaxCapacity - bookedSeats;

                BusDTO b = new BusDTO
                {
                    BusId = bus.BusId,
                    BusName = bus.BusName,
                    Source = bus.Source,
                    DepartureTime = bus.DepartureTime,
                    Destination = bus.Destination,
                    ArrivalTime = bus.ArrivalTime,
                    Fare = bus.Fare,
                    AvailableSeats = availableSeats,
                    BusSpecs = bus.BusSpecs
                };
                searchReault.Add(b);
            }

            return searchReault;
        }
    }
}
