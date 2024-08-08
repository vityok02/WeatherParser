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
            .Include(u => u.CurrentLocation)
            .ThenInclude(l => l.Coordinates)
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

    public async Task EnsureCreate(long userId, User user, CancellationToken cancellationToken)
    {
        var isUserExist = await _dbContext.Users.AnyAsync(u => u.Id == userId, cancellationToken);

        if (!isUserExist)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }
    }
}
