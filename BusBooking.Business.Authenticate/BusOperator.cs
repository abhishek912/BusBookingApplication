using BusBooking.Data.DAL;
using BusBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusBooking.Shared.DTO;

namespace BusBooking.Business.Authenticate
{
    public class BusOperator : IBusOperator
    {
        private readonly IReadData readObj;
        private readonly IWriteData writeObj;
        public BusOperator(IReadData readObj, IWriteData writeObj)
        {
            this.readObj = readObj;
            this.writeObj = writeObj;
        }

        public List<BusBookingDTO> GetBookingsOfBus(int busId)
        {
            var data = new List<BusBookingDTO>();
            var bookings = readObj.GetBookings().Where(x => x.BusId == busId).ToList(); 
            foreach(var booking in bookings)
            {
                var userContact = readObj.GetUsers().Where(x => x.UserId == booking.UserId).Select(x => x.Contact).SingleOrDefault();
                var psngrs = readObj.GetPassengers(booking.BookingId);

                string psngrsContact = "";
                foreach (var psngr in psngrs)
                {
                    psngrsContact += psngr.PContact + " ";
                }

                var d = new BusBookingDTO
                {
                    BookingId = booking.BookingId,
                    BookingDate = booking.BookingDate,
                    BookingTime = booking.BookingTime,
                    CurrentTimestamp = booking.CurrentTimestamp,
                    NoOfPassengers = booking.NoOfPassengers,
                    UserContact = userContact,
                    PsngersContact = psngrsContact,
                    AmountPaid = booking.AmountPaid,
                    Status = booking.Status                    
                };
                data.Add(d);
            }
            return data;
        }

        public List<BusServiceProviderDTO> GetBusByOperator(int id)
        {
            var buses = readObj.GetBusesOfOperator(id);
            List<BusServiceProviderDTO> busData = new List<BusServiceProviderDTO>();

            foreach(var busNo in buses)
            {
                var b = readObj.GetBusDetailsByBusNo(busNo);

                if (b != null)
                {
                    var busName = b.BusName;
                    var busId = b.BusId;
                    var busStatus = b.Status;
                    BusServiceProviderDTO data = new BusServiceProviderDTO
                    {
                        BusId = busId,
                        BusNo = busNo,
                        BusName = busName,
                        BusStatus = busStatus
                    };
                    busData.Add(data);
                }
            }

            return busData;
        }

        public BusDTO GetBusDetailsByBusId(int BusId)
        {
            var bus = readObj.GetBusDetailsByBusId(BusId);

            if(bus == null)
            {
                throw new Exception("GetBusDetailsByBusId: No bus found for this id.");
            }

            BusDTO busDetails = new BusDTO
            {
                BusNo = bus.BusNo,
                BusName = bus.BusName,
                Source = bus.Source,
                Destination = bus.Destination,
                DepartureTime = bus.DepartureTime,
                ArrivalTime = bus.ArrivalTime,
                Fare = bus.Fare,
                AvailableSeats = bus.MaxCapacity,
                BusSpecs = bus.BusSpecs
            };

            return busDetails;
        }

        public bool EditBusDetails(BusDTO bus)
        {
            if (bus == null)
            {
                throw new Exception("EditBusDetails: BusDTO is null");
            }

            Bus b = new Bus
            {
                BusId = bus.BusId,
                BusNo = bus.BusNo,
                BusName = bus.BusName,
                Source = bus.Source,
                Destination = bus.Destination,
                DepartureTime = bus.DepartureTime,
                ArrivalTime = bus.ArrivalTime,
                Fare = bus.Fare,
                MaxCapacity = bus.AvailableSeats,
                BusSpecs = bus.BusSpecs
            };

            var result = writeObj.EditBus(b);
            return result;
        }

        public string CreateBus(BusDTO bus)
        {
            if (BusExists(bus.BusNo))
            {
                //bus number already exists. Error code: 1
                return "false, " + "1";
            }

            Bus obj = new Bus
            {
                BusId = bus.BusId,
                BusNo = bus.BusNo,
                BusName = bus.BusName,
                Source = bus.Source,
                DepartureTime = bus.DepartureTime,
                Destination = bus.Destination,
                ArrivalTime = bus.ArrivalTime,
                Fare = bus.Fare,
                MaxCapacity = bus.AvailableSeats,
                BusSpecs = bus.BusSpecs
            };
            var result = writeObj.AddBus(obj);
            if(result)
            {
                writeObj.AddToBSP(bus.UserId, bus.BusNo);
            }
            else
            {
                //Bus not added. Error at database side. Error code: 2
                return "false, " + "2";
            }
            //Bus added successfully. Success Code: 3
            return "true, " + "3";
        }

        private bool BusExists(string busNo)
        {
            var result = readObj.GetBusDetailsByBusNo(busNo);
            if(result!=null && result.Status == 1)
            {
                return true;
            }
            return false;
        }

        public bool RemoveBus(int BusId)
        {
            bool result = false;

            var bookings = readObj.GetBookings().Where(x => x.BusId == BusId && x.Status == 1).ToList();
            var b = new List<Booking>();
            var date = DateTime.Now.Date;
            foreach(var booking in bookings)
            {
                if(DateTime.Parse(booking.BookingDate) >= date)
                {
                    return result;
                }
            }

            var bus = readObj.GetBusDetailsByBusId(BusId);
            var BusNo = bus.BusNo;
            result = writeObj.RemoveBusFromBuses(bus);
            if(result)
            {
                var busOfProvider = readObj.GetBusOfProviderByBusNo(BusNo);
                result = writeObj.RemoveBusFromBSP(busOfProvider);
            }

            return result;
        }
    }
}
