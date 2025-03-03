using BusBooking.Core.Dtos.Location;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var locations = _context.Locations
                .Select(l => new LocationDto
                {
                    LocationId = l.LocationId,
                    LocationName = l.LocationName,
                    Code = l.Code
                }).ToList();

            if (!locations.Any())
            {
                throw new KeyNotFoundException("No locations found.");
            }

            return locations;
        }

        public LocationDto GetLocationById(int locationId)
        {
            if (locationId <= 0)
            {
                throw new ArgumentException("Invalid LocationId. LocationId must be greater than 0.");
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with LocationId: {locationId} not found.");
            }

            return new LocationDto
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName,
                Code = location.Code
            };
        }

        public LocationDto CreateLocation(LocationDto locationDto)
        {
            if (locationDto == null)
            {
                throw new ArgumentNullException(nameof(locationDto), "LocationDto cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(locationDto.LocationName))
            {
                throw new ArgumentException("LocationName is required.");
            }

            if (string.IsNullOrWhiteSpace(locationDto.Code))
            {
                throw new ArgumentException("Code is required.");
            }

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
            if (locationId <= 0)
            {
                throw new ArgumentException("Invalid LocationId. LocationId must be greater than 0.");
            }

            if (locationDto == null)
            {
                throw new ArgumentNullException(nameof(locationDto), "LocationDto cannot be null.");
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with LocationId: {locationId} not found.");
            }

            location.LocationName = locationDto.LocationName;
            location.Code = locationDto.Code;

            _context.SaveChanges();
            return locationDto;
        }

        public void DeleteLocation(int locationId)
        {
            if (locationId <= 0)
            {
                throw new ArgumentException("Invalid LocationId. LocationId must be greater than 0.");
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with LocationId: {locationId} not found.");
            }

            _context.Locations.Remove(location);
            _context.SaveChanges();
        }
    }
}