using Microsoft.Extensions.Caching.Memory;
using WeatherParser.Models;

namespace WeatherParser.Data.Repositories.UserRepository;

public class CachedUserRepository : IUserRepository
{
    private readonly UserRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachedUserRepository(UserRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        => await _decorated.AddAsync(user, cancellationToken);

    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var key = $"user-{id}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _decorated.GetByIdAsync(id, cancellationToken);
            });
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        => await _decorated.UpdateAsync(user, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _decorated.SaveChangesAsync(cancellationToken);

    public async Task<string> GetUserLocationNameAsync(long Id, CancellationToken cancellationToken = default)
        => await _decorated.GetUserLocationNameAsync(Id, cancellationToken);

    public async Task<User?> GetUserWithLocationsAsync(long userId, CancellationToken cancellationToken = default)
        => await _decorated.GetUserWithLocationsAsync(userId, cancellationToken);
}