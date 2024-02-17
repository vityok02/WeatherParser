using Domain.CachedLocations;

namespace Application.Interfaces;

public interface ICachedPlacesRepository
{
    CachedLocation[]? GetCache(long userId);
    void SetCache(long userId, CachedLocation[] locations);
    void RemoveCache(long userId);
}
