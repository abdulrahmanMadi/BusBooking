using BusBooking.Core.Dtos.Location;


namespace BusBooking.Core.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<LocationDto> GetAllLocations();
        LocationDto GetLocationById(int locationId);
        LocationDto CreateLocation(LocationDto locationDto);
        LocationDto UpdateLocation(int locationId, LocationDto locationDto);
        void DeleteLocation(int locationId);
    }
}
