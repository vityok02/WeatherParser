using Domain.CachedLocations;

namespace Application.Interfaces;

public interface IPlacesRepository
{
    CachedLocation[]? GetPlaces(long userId);
    void SetPlaces(long userId, CachedLocation[] locations);
    void RemovePlaces(long userId);
}
