using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusBooking.Data.Models
{
    [Table("Buses")]
    public class Bus
    {
        [Key]
        public int BusId { get; set; }

        [Required]
        public string BusNo { get; set; }
        
        [Required]
        public string BusName{ get; set; }
        
        [Required]
        public string Source { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string ArrivalTime { get; set; }

        [Required]
        public decimal Fare { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        public string BusSpecs { get; set; }
        public int Status { get; set; } = 1;
    }
}
