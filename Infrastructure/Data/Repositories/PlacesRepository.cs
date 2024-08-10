using Application.Interfaces;
using Domain.CachedLocations;
using Infrastructure.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Repositories;

public class PlacesRepository : IPlacesRepository
{
    private readonly IMemoryCache _memoryCache;

    public PlacesRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public CachedLocation[]? GetPlaces(long userId)
        => _memoryCache.Get<CachedLocation[]>(CacheKeys.PlacesByUserId(userId));

    public void SetPlaces(long userId, CachedLocation[] locations)
        => _memoryCache.Set(CacheKeys.PlacesByUserId(userId), locations, TimeSpan.FromMinutes(2));

    public void RemovePlaces(long userId)
        => _memoryCache.Remove(CacheKeys.PlacesByUserId(userId));
}
