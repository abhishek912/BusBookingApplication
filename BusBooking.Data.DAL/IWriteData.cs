using BusBooking.Data.Models;
using System.Collections.Generic;

namespace BusBooking.Data.DAL
{
    public interface IWriteData
    {
        bool AddUser(User user);
        bool AddCredential(Credential cred);
        bool EditBus(Bus bus);
        int AddToBooking(Booking obj);
        bool AddToPassenger(int bookingId, List<Passenger> passengerDetails);

        bool AddBus(Bus bus);
        bool AddToBSP(int userId, string BusNo);
        bool RemoveBusFromBuses(Bus bus);
        bool RemoveBusFromBSP(BusServiceProvider bus);
        bool RemovePassenger(Passenger psngr);
        bool RemoveBooking(Booking booking);
        bool UpdateNoOfPassengers(int BookingId, int count);
        bool UpdateBookingFare(int BookingId, decimal amount);
    }
}