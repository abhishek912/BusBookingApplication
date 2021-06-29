using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusBooking.Data.Models
{
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BusId { get; set; }
        public int NoOfPassengers { get; set; } = 1;

        [Required]
        public string BookingDate { get; set; }

        [Required]
        public string BookingTime { get; set; }

        public DateTime CurrentTimestamp { get; set; } = DateTime.Now;

        public Decimal AmountPaid { get; set; }
        public int Status { get; set; } = 1;
    }
}
