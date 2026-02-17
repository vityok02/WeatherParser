using Domain.Locations;

namespace Domain.CachedLocations;

public static class CachedLocationExtensions
{
    public static CachedLocation ToCachedLocation(this Location location)
    {
        return new CachedLocation(
            location.Name,
            location.Coordinates);
    }
}
