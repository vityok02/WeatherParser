using Application.Interfaces;
using Infrastructure.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Repositories;

public class CachedUserStateRepository : ICachedUserStateRepository
{
    private readonly IMemoryCache _memoryCache;

    public CachedUserStateRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string? GetCache(long userId)
        => _memoryCache.Get<string>(CacheKeys.UserStateByUserId(userId));

    public void SetCache(long userId, string state)
        => _memoryCache.Set(CacheKeys.UserStateByUserId(userId), state, TimeSpan.FromMinutes(2));

    public void RemoveCache(long userId)
        => _memoryCache.Remove(CacheKeys.UserStateByUserId(userId));
}
