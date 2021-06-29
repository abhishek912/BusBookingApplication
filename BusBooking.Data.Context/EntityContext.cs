using BusBooking.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusBooking.Data.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusServiceProvider> BusServiceProviders { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Log> Log { get; set; }
    }
}
