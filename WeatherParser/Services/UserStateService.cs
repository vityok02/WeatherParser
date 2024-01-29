using Microsoft.Extensions.Caching.Memory;
using WeatherParser.Data.Repositories.CacheRepositories;
using WeatherParser.Models.Interfaces;

namespace WeatherParser.Services;

public class UserStateService : IUserStateService
{
    private readonly UserStateCacheRepository _cacheRepository;

    public UserStateService(UserStateCacheRepository cacheManager)
    {
        _cacheRepository = cacheManager;
    }

    public void SetUserState(long userId, string state)
    {
        _cacheRepository.SetCache(userId, state, new MemoryCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(5)
        });
    }

    public string? GetUserState(long userId)
    {
        return _cacheRepository.GetCache<string?>(userId);
    }

    public void RemoveUserState(long userId)
    {
        _cacheRepository.RemoveCache(userId);
    }

    public void ChangeUserState(long userId, string userState)
    {
        RemoveUserState(userId);
        SetUserState(userId, userState);
    }
}
