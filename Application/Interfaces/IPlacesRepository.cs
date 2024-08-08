using Domain.CachedLocations;

namespace Application.Interfaces;

public interface IPlacesRepository
{
    CachedLocation[]? GetCache(long userId);
    void SetCache(long userId, CachedLocation[] locations);
    void RemoveCache(long userId);
}
