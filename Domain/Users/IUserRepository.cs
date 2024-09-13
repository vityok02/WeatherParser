using Domain.Languages;

namespace Domain.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<User?> GetByIdWithLocationsAsync(long id, CancellationToken cancellationToken);
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<bool> HasLocationAsync(long Id, CancellationToken cancellationToken);
    Task EnsureCreateAsync(long userId, CancellationToken cancellationToken);
    Task<Language?> GetLanguageAsync(long userId, CancellationToken cancellationToken);
}
