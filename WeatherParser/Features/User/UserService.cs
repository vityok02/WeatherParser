using Microsoft.Extensions.Caching.Memory;
using WeatherParser.Data.Repositories.UserRepository;

namespace WeatherParser.Features.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;

    public UserService(IUserRepository userRepository, IMemoryCache memoryCache)
    {
        _userRepository = userRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Models.User?> GetUserAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task SetUserLocationAsync(
        long userId,
        Models.Location location,
        CancellationToken cancellationToken = default)
    {
        var user = await GetUserWithLocationsAsync(userId, cancellationToken);

        if (user!.Locations!.Any(l => l.Name == location.Name))
        {
            user.Locations!.Add(location);
        }

        user!.SetCurrentLocation(location!);
        await _userRepository.UpdateAsync(user, cancellationToken);
    }

    public async Task<string> GetUserLocationNameAsync(long userId, CancellationToken cancellationToken = default)
    {
        var userLocationName = await _userRepository.GetUserLocationNameAsync(userId, cancellationToken);

        return userLocationName;
    }

    public async Task<Models.User> GetUserWithLocationsAsync(long userId, CancellationToken cancellationToken)
    {
        _memoryCache.TryGetValue(userId, out Models.User? user);
        if (user is not null)
        {
            return user;
        }

        user = await _userRepository.GetUserWithLocationsAsync(userId, cancellationToken);
        if (user is not null)
        {
            _memoryCache.Set(userId, user, new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });
        }

        return user!;
    }

    public async Task<bool> IsUserInDbAsync(long id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        return user is not null;
    }

    public async Task AddUserToDbAsync(Models.User user, CancellationToken cancellationToken)
    {
        await _userRepository.AddAsync(user, cancellationToken);
    }
}
