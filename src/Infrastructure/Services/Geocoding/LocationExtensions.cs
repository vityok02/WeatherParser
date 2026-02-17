using Domain.Locations;
using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Infrastructure.Services.Geocoding;

public static class LocationExtensions
{
    public static Location ToAppLocation(this LocationResponse response)
    {
        return new Location(
            response.FullPlaceName,
            new Coordinates(
                response.Center[0], 
                response.Center[1]
            )
        );
    }
}
