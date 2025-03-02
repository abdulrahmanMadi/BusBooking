using BusBooking.Core.Dtos.Location;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;


namespace BusBooking.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LocationDto> GetAllLocations()
        {
            return _context.Locations.Select(l => new LocationDto
            {
                LocationId = l.LocationId,
                LocationName = l.LocationName,
                Code = l.Code
            }).ToList();
        }

        public LocationDto GetLocationById(int locationId)
        {
            var location = _context.Locations.FirstOrDefault(l => l.LocationId == locationId);
            return location == null ? null : new LocationDto
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName,
                Code = location.Code
            };
        }

        public LocationDto CreateLocation(LocationDto locationDto)
        {
            var location = new Location
            {
                LocationName = locationDto.LocationName,
                Code = locationDto.Code
            };

            _context.Locations.Add(location);
            _context.SaveChanges();

            locationDto.LocationId = location.LocationId;
            return locationDto;
        }

        public LocationDto UpdateLocation(int locationId, LocationDto locationDto)
        {
            var location = _context.Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (location == null) return null;

            location.LocationName = locationDto.LocationName;
            location.Code = locationDto.Code;

            _context.SaveChanges();
            return locationDto;
        }

        public void DeleteLocation(int locationId)
        {
            var location = _context.Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (location != null)
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
            }
        }
    }
}