using BusBooking.Data.Models;
using System.Collections.Generic;

namespace BusBooking.Data.DAL
{
    public interface IReadData
    {
        List<Credential> GetCredentials();
        List<User> GetUsers();
        List<string> GetBusesOfOperator(int id);
        Bus GetBusDetailsByBusNo(string BusNo);
        Bus GetBusDetailsByBusId(int BusId);
        List<Booking> GetBookings();
        List<Passenger> GetPassengers(int BookingId);
        Passenger GetPassengerByPsngrId(int PsngrId);
        BusServiceProvider GetBusOfProviderByBusNo(string BusNo);

        List<Bus> GetBuses();
    }
}