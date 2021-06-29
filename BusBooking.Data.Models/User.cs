using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusBooking.Data.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [RegularExpression("^[A-Za-z ]+")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]+")]
        [MinLength(8)]
        [MaxLength(10)]
        public string Contact { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int Admin { get; set; } = 0;
    }
}
