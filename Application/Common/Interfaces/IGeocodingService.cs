using Domain.Abstract;
using Domain.Locations;

namespace Application.Common.Interfaces;

public interface IGeocodingService
{
    Task<Result<Location[]>> GetPlacesByName(string placeName, CancellationToken cancellationToken);
    Task<Result<string>> GetPlaceName(Coordinates coordinates, CancellationToken cancellationToken);
}
