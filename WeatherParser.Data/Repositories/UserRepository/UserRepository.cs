using Microsoft.EntityFrameworkCore;
using WeatherParser.Models;

namespace WeatherParser.Data.Repositories.UserRepository;

public sealed class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserWithLocationsAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Locations)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string> GetUserLocationNameAsync(long Id, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == Id)
            .Include(u => u.CurrentLocation)
            .FirstOrDefaultAsync(cancellationToken);

        return user!.CurrentLocation!.Name!;
    }
}
