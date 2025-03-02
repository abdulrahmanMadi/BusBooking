using BusBooking.Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace BusBooking.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data (optional)
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "admin", EmailId = "admin@example.com", FullName = "Admin User", Role = "Admin", CreatedDate = DateTime.UtcNow, Password = "admin123", ProjectName = "BusBooking" },
                new User { UserId = 2, UserName = "customer", EmailId = "customer@example.com", FullName = "Customer User", Role = "Customer", CreatedDate = DateTime.UtcNow, Password = "customer123", ProjectName = "BusBooking" }
            );
        }
    }
}