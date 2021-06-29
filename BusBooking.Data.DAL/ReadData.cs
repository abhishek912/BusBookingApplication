using BusBooking.Data.Context;
using BusBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBooking.Data.DAL
{
    public class ReadData : IReadData
    {
        private readonly EntityContext Context;
        public ReadData(EntityContext _context)
        {
            Context = _context;
        }

        public List<Credential> GetCredentials()
        {
            var cred = Context.Credentials.ToList();
            return cred;
        }

        public List<User> GetUsers()
        {
            var users = Context.Users.ToList();
            return users;
        }

        public List<string> GetBusesOfOperator(int id)
        {
            var buses = Context.BusServiceProviders.Where(x => x.UserId == id && x.BusStatus == 1).Select(x => x.BusNo).ToList();
            return buses;
        }
        public Bus GetBusDetailsByBusNo(string BusNo)
        {
            var bus = Context.Buses.Where(bus => bus.BusNo == BusNo && bus.Status == 1).FirstOrDefault();
            return bus;
        }

        public Bus GetBusDetailsByBusId(int BusId)
        {
            var bus = Context.Buses.Where(bus => bus.BusId == BusId).FirstOrDefault();
            if (bus == null)
            {
                throw new Exception("GetBusDetailsByBusId: No bus found for this id.");
            }
            return bus;
        }

        public List<Booking> GetBookings()
        {
            var bookings = Context.Bookings.ToList();
            return bookings;
        }

        public List<Passenger> GetPassengers(int BookingId)
        {
            var passengers = Context.Passengers.Where(x => x.BookingId == BookingId).ToList();
            return passengers;
        }

        public BusServiceProvider GetBusOfProviderByBusNo(string BusNo)
        {
            var bus = Context.BusServiceProviders.Where(x => x.BusNo.Equals(BusNo)).SingleOrDefault();
            return bus;
        }

        public List<Bus> GetBuses()
        {
            var buses = Context.Buses.ToList();
            return buses;
        }

        public Passenger GetPassengerByPsngrId(int PsngrId)
        {
            var psngr = Context.Passengers.Where(x => x.PsngrId == PsngrId).SingleOrDefault();
            if (psngr == null)
            {
                throw new Exception("GetPassengerByPsngrId: No Psngr found for this id.");
            }
            return psngr;
        }
    }
}
