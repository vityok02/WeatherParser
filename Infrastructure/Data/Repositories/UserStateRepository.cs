using Application.Common.Interfaces.Repositories;
using Common.Constants;
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

    public UserState? GetState(long userId)
        => _memoryCache.Get<UserState>(CacheKeys.UserStateByUserId(userId));

    public void SetState(long userId, UserState state)
    {
        _memoryCache.Remove(userId);
        _memoryCache.Set(CacheKeys.UserStateByUserId(userId), state, TimeSpan.FromMinutes(2));
    }

    public void RemoveState(long userId)
        => _memoryCache.Remove(CacheKeys.UserStateByUserId(userId));
}
