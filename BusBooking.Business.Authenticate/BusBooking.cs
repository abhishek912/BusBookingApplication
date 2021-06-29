using BusBooking.Data.DAL;
using BusBooking.Data.Models;
using BusBooking.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Business.Authenticate
{
    public class BusBooking : IBusBooking
    {
        private readonly IReadData readObj;
        private readonly IWriteData writeObj;
        public BusBooking(IReadData readObj, IWriteData writeObj)
        {
            this.readObj = readObj;
            this.writeObj = writeObj;
        }

        public List<BookingSummaryDTO> GetBookings(int userId)
        {
            List<BookingSummaryDTO> trips = new List<BookingSummaryDTO>();

            var bookings = readObj.GetBookings().Where(x => x.UserId == userId).ToList();
            
            foreach(var booking in bookings)
            {
                var bus = readObj.GetBusDetailsByBusId(booking.BusId);
                BookingSummaryDTO b = new BookingSummaryDTO
                {
                    BookingId = booking.BookingId,
                    BusName = bus.BusName,
                    Source = bus.Source,
                    DepartureTime = bus.DepartureTime,
                    Destination = bus.Destination,
                    ArrivalTime = bus.ArrivalTime,
                    BookingDate = booking.BookingDate,
                    NoOfPassengers = booking.NoOfPassengers,
                    TotalFare = booking.AmountPaid,
                    Status = booking.Status
                };
                trips.Add(b);
            }
            return trips;
        }

        public BookingDetailDTO GetBookingDetails(int BookingId)
        {
            var booking = readObj.GetBookings().Where(x => x.BookingId == BookingId).SingleOrDefault();

            if (booking == null)
            {
                throw new NullReferenceException("GetBookingDetails: Booking does not exist.");
            }

            var user = readObj.GetUsers().Where(x => x.UserId == booking.UserId).SingleOrDefault();

            if (user == null)
            {
                throw new NullReferenceException("GetBookingDetails: User does not exist.");
            }

            var bus = readObj.GetBusDetailsByBusId(booking.BusId);

            if (bus == null)
            {
                throw new NullReferenceException("GetBookingDetails: Bus Does not exist.");
            }

            var psngrs = readObj.GetPassengers(BookingId);

            BookingDetailDTO tripDetail = new BookingDetailDTO 
            {
                BookingId = booking.BookingId,
                BookedBy = user.FullName,
                BookingContact = user.Contact,
                BookingEmail = user.Email,
                BusName = bus.BusName,
                BusSpecs = bus.BusSpecs,
                Source = bus.Source,
                DepartureTime = bus.DepartureTime,
                Destination = bus.Destination,
                ArrivalTime = bus.ArrivalTime,
                BookingDate = booking.BookingDate,
                TicketBookedOn = booking.CurrentTimestamp,
                NoOfPassengers = booking.NoOfPassengers,
                TotalFare = booking.AmountPaid,
                Status = booking.Status,
                PassengerList = psngrs
            };

            return tripDetail;
        }

        public bool CreateBooking(BookingTicketDTO ticket)
        {
            var BookingObj = new Booking
            {
                UserId = ticket.UserId,
                BusId = ticket.BusId,
                NoOfPassengers = ticket.NoOfPassengers,
                BookingDate = ticket.BookingDate,
                BookingTime = ticket.BookingTime,
                AmountPaid = ticket.AmountPaid
            };

            int BookingId = writeObj.AddToBooking(BookingObj);
            bool result = writeObj.AddToPassenger(BookingId, ticket.PassengerDetails);

            return result;
        }

        public bool CancelBooking(int BookingId)
        {
            var psngrs = readObj.GetPassengers(BookingId);

            if(psngrs.Count == 0)
            {
                throw new Exception("No Passenger found for this booking.");
            }

            foreach(var psngr in psngrs)
            {
                writeObj.RemovePassenger(psngr);
            }

            var booking = readObj.GetBookings().Where(x => x.BookingId == BookingId).SingleOrDefault();

            if (booking == null)
            {
                throw new NullReferenceException("No booking exist to cancel the booking.");
            }

            var result = writeObj.RemoveBooking(booking);

            return result;
        }

        public bool CancelPassengersBooking(int BookingId, List<int> PsngrId, bool refund)
        {
            bool result = true;
            if(readObj.GetBookings().Where(x=>x.BookingId == BookingId).Select(x=>x.NoOfPassengers).SingleOrDefault() == PsngrId.Count)
            {
                result = CancelBooking(BookingId);
                return result;
            }

            foreach(var id in PsngrId)
            {
                if(result)
                {
                    var psngr = readObj.GetPassengerByPsngrId(id);
                    result = writeObj.RemovePassenger(psngr); 
                }
                else
                {
                    break;
                }
            }

            int busId = readObj.GetBookings().Where(x=>x.BookingId == BookingId).Select(x=>x.BusId).SingleOrDefault();
            writeObj.UpdateNoOfPassengers(BookingId, PsngrId.Count);

            if(refund)
            {
                decimal ticketFare = readObj.GetBusDetailsByBusId(busId).Fare;
                writeObj.UpdateBookingFare(BookingId, ticketFare * PsngrId.Count);
            }

            return result;
        }
    }
}
