using BusBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Shared.DTO
{
    public class BookingDetailDTO
    {
        public int BookingId { get; set; }
        public string BookedBy { get; set; }
        public string BookingContact { get; set; }
        public string BookingEmail { get; set; }
        public string BusName { get; set; }
        public string BusSpecs { get; set; }
        public string Source { get; set; }
        public string DepartureTime { get; set; }
        public string Destination { get; set; }
        public string ArrivalTime { get; set; }
        public string BookingDate { get; set; }
        public DateTime TicketBookedOn { get; set; }
        public int NoOfPassengers { get; set; }
        public decimal TotalFare { get; set; }
        public int Status { get; set; }
        public List<Passenger> PassengerList { get; set; }
    }
}
