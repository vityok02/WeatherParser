using Application.Common.Interfaces;
using Application.Users;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public class SessionManager : ISessionManager
{
    private readonly IMemoryCache _cache;

    public SessionManager(IMemoryCache cache)
    {
        _cache = cache;
    }

    public UserSession GetOrCreateSession(long userId)
    {
        if (!_cache.TryGetValue(userId, out UserSession? session))
        {
            session = new UserSession(userId);
            var entryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(userId, session, entryOptions);
        }

        return session!;
    }

    public void RemoveSession(long userId)
    {
        _cache.Remove(userId);
    }
}