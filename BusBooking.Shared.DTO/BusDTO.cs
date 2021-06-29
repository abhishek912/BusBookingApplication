using System;

namespace BusBooking.Shared.DTO
{
    public class BusDTO
    {
        public int UserId { get; set; }
        public int BusId { get; set; }
        public string BusNo { get; set; }
        public string BusName { get; set; }

        public string Source { get; set; }

        public string DepartureTime { get; set; }

        public string Destination { get; set; }

        public string ArrivalTime { get; set; }

        public decimal Fare { get; set; }
        public int AvailableSeats { get; set; }

        public string BusSpecs { get; set; }
    }
}
