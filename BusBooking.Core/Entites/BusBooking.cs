

namespace BusBooking.Core.Entites
{
    public class BusBooking
    {
        public int BookingId { get; set; } // Primary key
        public int CustId { get; set; } // Foreign key to User (Customer)
        public User Customer { get; set; } // Navigation property
        public DateTime BookingDate { get; set; }
        public int ScheduleId { get; set; } // Foreign key to BusSchedule
        public BusSchedule Schedule { get; set; } // Navigation property
        public List<BusBookingPassenger> BusBookingPassengers { get; set; } // One-to-many relationship
    }
}
