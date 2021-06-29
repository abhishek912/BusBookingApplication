using BusBooking.Data.Context;
using BusBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Data.DAL
{
    public class WriteData : IWriteData
    {
        private readonly EntityContext Context;
        public WriteData(EntityContext Context)
        {
            this.Context = Context;
        }

        public bool AddUser(User user)
        {
            Context.Users.Add(user);
            if (Context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool AddCredential(Credential cred)
        {
            Context.Credentials.Add(cred);
            if (Context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("Credentials cannot be added to the table");
            }
        }

        public bool EditBus(Bus bus)
        {
            var oldBus = Context.Buses.Where(x => x.BusId == bus.BusId).FirstOrDefault();
            if (oldBus == null)
            {
                throw new Exception("No Bus number found.");
            }
            string oldBusNo = oldBus.BusNo;

            oldBus.BusNo = bus.BusNo;
            oldBus.BusName = bus.BusName;
            oldBus.ArrivalTime = bus.ArrivalTime;
            oldBus.DepartureTime = bus.DepartureTime;
            oldBus.MaxCapacity = bus.MaxCapacity;
            oldBus.Source = bus.Source;
            oldBus.Destination = bus.Destination;
            oldBus.BusSpecs = bus.BusSpecs;
            oldBus.Fare = bus.Fare;

            var provider = Context.BusServiceProviders.Where(x => x.BusNo.Equals(oldBusNo)).FirstOrDefault();
            if(provider == null)
            {
                throw new Exception("No bus service provider is found.");
            }
            provider.BusNo = bus.BusNo;

            return Context.SaveChanges() > 0;
        }

        public int AddToBooking(Booking obj)
        {
            Context.Bookings.Add(obj);
            Context.SaveChanges();

            int bookingId = Context.Bookings.OrderBy(x=>x.BookingId).Where(x => x.UserId == obj.UserId).Select(x => x.BookingId).LastOrDefault();
            return bookingId;
        }

        public bool AddToPassenger(int BookingId, List<Passenger> passengerDetails)
        {
            foreach(var passenger in passengerDetails)
            {
                passenger.BookingId = BookingId;
                Context.Passengers.Add(passenger);
            }

            return Context.SaveChanges() > 0;
        }

        public bool AddBus(Bus bus)
        {
            Context.Buses.Add(bus);
            return Context.SaveChanges() > 0;
        }

        public bool AddToBSP(int userId, string BusNo)
        {
            var bus = new BusServiceProvider { UserId = userId, BusNo = BusNo };
            Context.BusServiceProviders.Add(bus);
            return Context.SaveChanges() > 0;
        }

        public bool RemoveBusFromBuses(Bus bus)
        {
            var b = Context.Buses.Where(x => x.BusId == bus.BusId).FirstOrDefault();
            b.Status = 0;
            return Context.SaveChanges() > 0;
        }

        public bool RemoveBusFromBSP(BusServiceProvider bus)
        {
            var b = Context.BusServiceProviders.Where(x => x.BusNo.Equals(bus.BusNo)).FirstOrDefault();
            b.BusStatus = 0;
            return Context.SaveChanges() > 0;
        }

        public bool RemovePassenger(Passenger psngr)
        {
            var p = Context.Passengers.Where(x=>x.PsngrId==psngr.PsngrId).SingleOrDefault();
            if (p == null)
            {
                throw new Exception("Passenger cannot be updated. because no passenger is found.");
            }
            p.Status = 0;
            return Context.SaveChanges() > 0;
        }

        public bool RemoveBooking(Booking booking)
        {
            var b = Context.Bookings.Where(x => x.BookingId == booking.BookingId).SingleOrDefault();
            if (b == null)
            {
                throw new Exception("Booking cannot be removed. because no booking is found.");
            }
            b.Status = 0;
            return Context.SaveChanges() > 0;
        }

        public bool UpdateNoOfPassengers(int BookingId, int count)
        {
            var booking = Context.Bookings.Where(x => x.BookingId == BookingId).SingleOrDefault();
            if (booking == null)
            {
                throw new Exception("Passengers cannot be updated. because no booking id is found.");
            }
            booking.NoOfPassengers -= count;
            return Context.SaveChanges() > 0;
        }

        public bool UpdateBookingFare(int BookingId, decimal amount)
        {
            var booking = Context.Bookings.Where(x => x.BookingId == BookingId).SingleOrDefault();
            if(booking == null)
            {
                throw new Exception("Amount cannot be updated. because no booking id is found.");
            }
            booking.AmountPaid -= amount;
            return Context.SaveChanges() > 0;
        }
    }
}
