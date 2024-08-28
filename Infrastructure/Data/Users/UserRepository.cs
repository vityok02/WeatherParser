using Domain.Languages;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Users;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Users.ToArrayAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FindAsync(id, cancellationToken);
    }

    public async Task<User?> GetByIdWithLocationsAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Where(u => u.Id == id)
            .Include(u => u.Locations).ThenInclude(l => l.Coordinates)
            .Include(u => u.CurrentLocation).ThenInclude(l => l.Coordinates)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> HasLocationAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(u => u.Id == id && u.CurrentLocation != null, cancellationToken);
    }

    public async Task EnsureCreateAsync(long userId, CancellationToken cancellationToken)
    {
        var users = _dbContext.Users;

        var isUserExist = await users.AnyAsync(u => u.Id == userId, cancellationToken);

        if (!isUserExist)
        {
            var user = new User(userId, 1);

            await CreateAsync(user, cancellationToken);
        }
    }

    public async Task<Language?> GetLanguageAsync(long id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id)
            .Include(u => u.Language)
            .FirstOrDefaultAsync(cancellationToken);

        return user?.Language;
    }
}
