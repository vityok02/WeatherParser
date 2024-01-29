using WeatherParser.Features.Geocoding.GeocodingRecords;
using WeatherParser.Models;

namespace WeatherParser.Features.GeocodingServices;

public interface IGeocodingService
{
    Task<Result<Feature[]>> GetLocationsByName(string locationName, CancellationToken cancellationToken = default);
}
