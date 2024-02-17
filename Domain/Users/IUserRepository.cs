namespace Domain.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<User?> GetByIdWithLocationsAsync(long id, CancellationToken cancellationToken);
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<bool> HasLocationAsync(long Id, CancellationToken cancellationToken);
    Task EnsureCreate(long userId, User user, CancellationToken cancellationToken);
}
