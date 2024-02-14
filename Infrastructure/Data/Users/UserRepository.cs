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

    public IQueryable<User> GetQueryable()
    {
        return _dbContext.Users.AsQueryable();
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Users.ToArrayAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FindAsync(id, cancellationToken);
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
        return await _dbContext.Users.AnyAsync(u => u.Id == id && u.HasLocation, cancellationToken);
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
