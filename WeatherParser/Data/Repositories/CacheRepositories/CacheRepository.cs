using Microsoft.Extensions.Caching.Memory;

namespace WeatherParser.Data.Repositories.CacheRepositories;

public class CacheRepository
{
    private readonly IMemoryCache _memoryCache;
    protected string? _salt;

    protected CacheRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _salt = default;
    }

    public void SetCache<T>(long id, T value, MemoryCacheEntryOptions options = default!)
    {
        _memoryCache.Set(id + _salt, value);
    }

    public T? GetCache<T>(long id)
    {
        return _memoryCache.Get<T>(id + _salt);
    }

    public void RemoveCache(long id)
    {
        _memoryCache.Remove(id + _salt);
    }
}

public class UserStateCacheRepository : CacheRepository
{
    public UserStateCacheRepository(IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _salt = "UserState";
    }
}

public class LocationsCacheRepository : CacheRepository
{
    public LocationsCacheRepository(IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _salt = "Locations";
    }
}