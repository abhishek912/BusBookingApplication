using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Data.Models
{
    [Table("BusServiceProvider")]
    public class BusServiceProvider
    {
        [Key]
        public int PId { get; set; }
        public int UserId { get; set; }
        public string BusNo { get; set; }
        public int BusStatus { get; set; } = 1;
    }
}
