using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusBooking.Data.Models
{
    [Table("Credentials")]
    public class Credential
    {
        [Key]
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
