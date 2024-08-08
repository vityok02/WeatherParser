using Application.Interfaces;
using Infrastructure.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Repositories;

public class UserStateRepository : IUserStateRepository
{
    private readonly IMemoryCache _memoryCache;

    public UserStateRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string? GetState(long userId)
        => _memoryCache.Get<string>(CacheKeys.UserStateByUserId(userId));

    public void SetState(long userId, string state)
        => _memoryCache.Set(CacheKeys.UserStateByUserId(userId), state, TimeSpan.FromMinutes(2));

    public void RemoveState(long userId)
        => _memoryCache.Remove(CacheKeys.UserStateByUserId(userId));
}
