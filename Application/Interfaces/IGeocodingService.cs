using Domain.Abstract;
using Domain.Locations;
using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Application.Interfaces;

public interface IGeocodingService
{
    Task<Result<Feature[]>> GetPlacesByName(string placeName, CancellationToken cancellationToken);
    Task<Result<string>> GetPlaceName(Coordinates coordinates, CancellationToken cancellationToken);
}
