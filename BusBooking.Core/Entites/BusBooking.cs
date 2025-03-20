

namespace BusBooking.Core.Entites
{
    public class BusBooking
    {
        public int BookingId { get; set; } 
        public int CustId { get; set; } 
        public User Customer { get; set; } 
        public DateTime BookingDate { get; set; }
        public int ScheduleId { get; set; } 
        public BusSchedule Schedule { get; set; }
        public List<BusBookingPassenger> BusBookingPassengers { get; set; } 
    }
}
