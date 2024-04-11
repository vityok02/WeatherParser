using Application.Interfaces;
using Infrastructure.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Repositories;

public class UserStateRepository : IUserStateRepository
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromMinutes(2);
    private readonly IMemoryCache _memoryCache;

    public UserStateRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string? GetUserState(long userId)
        => _memoryCache.Get<string>(CacheKeys.UserStateByUserId(userId));

    public void SetUserState(long userId, string state)
        => _memoryCache.Set(CacheKeys.UserStateByUserId(userId), state, CacheTime);

    public void RemoveUserState(long userId)
        => _memoryCache.Remove(CacheKeys.UserStateByUserId(userId));

    public void UpdateUserState(long userId, string state)
    {
        _memoryCache.Remove(CacheKeys.UserStateByUserId(userId));
        _memoryCache.Set(CacheKeys.UserStateByUserId(userId), state, CacheTime);
    }
}
