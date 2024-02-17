using Domain;
using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Application.Interfaces;

public interface IGeocodingService
{
    Task<Result<Feature[]>> GetPlacesByName(string placeName, CancellationToken cancellationToken);
}
