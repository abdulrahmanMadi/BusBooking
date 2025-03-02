using BusBooking.Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace BusBooking.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BusBooking.Core.Entites.BusBooking> BusBookings { get; set; }
        public DbSet<BusBookingPassenger> BusBookingPassengers { get; set; }
        public DbSet<BusSchedule> BusSchedules { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationAddress> LocationAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary keys
            modelBuilder.Entity<BusBooking.Core.Entites.BusBooking>().HasKey(b => b.BookingId);
            modelBuilder.Entity<BusBookingPassenger>().HasKey(p => new { p.PassengerId, p.BookingId }); // Composite key
            modelBuilder.Entity<BusSchedule>().HasKey(s => s.ScheduleId);
            modelBuilder.Entity<Location>().HasKey(l => l.LocationId);
            modelBuilder.Entity<LocationAddress>().HasKey(la => la.LocationPointId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            // Configure relationships
            modelBuilder.Entity<BusBooking.Core.Entites.BusBooking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusBooking.Core.Entites.BusBooking>()
                .HasOne(b => b.Schedule)
                .WithMany()
                .HasForeignKey(b => b.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusBookingPassenger>()
                .HasOne(p => p.BusBooking)
                .WithMany(b => b.BusBookingPassengers)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BusSchedule>()
                .HasOne(s => s.Vendor)
                .WithMany()
                .HasForeignKey(s => s.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure FromLocation relationship
            modelBuilder.Entity<BusSchedule>()
                .HasOne(s => s.FromLocation)
                .WithMany()
                .HasForeignKey(s => s.FromLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure ToLocation relationship
            modelBuilder.Entity<BusSchedule>()
                .HasOne(s => s.ToLocation)
                .WithMany()
                .HasForeignKey(s => s.ToLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LocationAddress>()
                .HasOne(la => la.Location)
                .WithMany()
                .HasForeignKey(la => la.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    }