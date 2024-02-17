using Domain.Users;
using Infrastructure.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Users;

public class CachedUserRepository : IUserRepository
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromMinutes(2);
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedUserRepository(UserRepository userRepository, AppDbContext dbContext, IMemoryCache memoryCache)
    {
        _userRepository = userRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.UserById(id),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);

                return _userRepository.GetByIdAsync(id, cancellationToken);
            });
    }

    public async Task<User?> GetByIdWithLocationsAsync(long id, CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(
            CacheKeys.UserById(id),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);

                return _userRepository.GetByIdWithLocationsAsync(id, cancellationToken);
            });
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _userRepository.CreateAsync(user, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> HasLocationAsync(long id, CancellationToken cancellationToken)
    {
        return await _userRepository.HasLocationAsync(id, cancellationToken);
    }

    public async Task EnsureCreate(long userId, User user, CancellationToken cancellationToken)
    {
        await _userRepository.EnsureCreate(userId, user, cancellationToken);
    }
}
