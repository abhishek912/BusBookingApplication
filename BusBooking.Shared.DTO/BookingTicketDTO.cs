using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking.Data.Models;

namespace BusBooking.Shared.DTO
{
    public class BookingTicketDTO
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public int BusId { get; set; }
        public int NoOfPassengers { get; set; } = 1;

        public string BookingDate { get; set; }

        public string BookingTime { get; set; }

        public Decimal AmountPaid { get; set; }

        public List<Passenger> PassengerDetails { get; set; }
    }
}
