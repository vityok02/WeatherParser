using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Application.UserStates;

public class CachedUserStateRepository : ICachedUserStateRepository
{
    private readonly IMemoryCache _memoryCache;
    private static readonly string _salt = "user-state";

    public CachedUserStateRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string? GetCache(long userId)
        => _memoryCache.Get<string>($"{_salt}-{userId}");

    public void SetCache(long userId, string state)
        => _memoryCache.Set($"{_salt}-{userId}", state, TimeSpan.FromMinutes(2));

    public void RemoveCache(long userId)
        => _memoryCache.Remove($"{_salt}-{userId}");
}
