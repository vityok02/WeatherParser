using WeatherParser.Models;
using WeatherParser.Models.GeocodingRecords;

namespace WeatherParser.Services.GeocodingServices;

public interface IGeocodingService
{
    Task<Result<Feature[]>> GetLocationsByName(string locationName, CancellationToken cancellationToken = default);
}
