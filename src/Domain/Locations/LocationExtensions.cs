using Domain.CachedLocations;

namespace Domain.Locations;

public static class LocationExtensions
{
    public static Location ToAppLocation(this CachedLocation cachedLocation)
    {
        return new Location(
            cachedLocation.PlaceName,
            cachedLocation.Coordinates);
    }
}
