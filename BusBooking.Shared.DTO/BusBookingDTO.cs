using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Shared.DTO
{
    public class BusBookingDTO
    {
        public int BookingId { get; set; }
        public string BookingDate { get; set; }
        public string BookingTime { get; set; }
        public DateTime CurrentTimestamp { get; set; } = DateTime.Now;
        public int NoOfPassengers { get; set; }
        public string UserContact { get; set; }
        public string PsngersContact { get; set; }
        public Decimal AmountPaid { get; set; }
        public int Status { get; set; }
    }
}
