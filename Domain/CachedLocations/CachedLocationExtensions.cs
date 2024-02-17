using Domain.Locations;
using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Domain.CachedLocations;

public static class CachedLocationExtensions
{
    public static CachedLocation ToCachedLocaion(this Feature feature)
    {
        var coordinates = new Coordinates(feature.Center[0], feature.Center[1]);

        return new CachedLocation(
            feature.FullPlaceName,
            coordinates);
    }
}
