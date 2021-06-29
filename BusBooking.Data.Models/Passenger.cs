using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusBooking.Data.Models
{
    [Table("Passengers")]
    public class Passenger
    {
        [Key]
        public int PsngrId { get; set; }
        [Required]
        [ForeignKey("Bookings")]
        public int BookingId { get; set; }

        [Required]
        public string PName { get; set; }
        
        [Required]
        public string PContact { get; set; }
        public string PEmail { get; set; }

        [Required]
        public int PsngrAge { get; set; }
        public int Status { get; set; } = 1;
    }
}
