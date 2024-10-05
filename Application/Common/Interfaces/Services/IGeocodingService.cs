using Domain.Abstract;
using Domain.Locations;

namespace Application.Common.Interfaces.Services;

public interface IGeocodingService
{
    Task<Result<Location[]>> GetPlacesByName(
        string placeName, CancellationToken cancellationToken);
    Task<Result<string>> GetPlaceName(
        Coordinates coordinates, string languageCode, CancellationToken cancellationToken);
}
