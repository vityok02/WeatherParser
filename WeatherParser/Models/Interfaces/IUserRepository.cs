namespace WeatherParser.Models.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<string> GetUserLocationNameAsync(long Id, CancellationToken cancellationToken = default);
    Task<User?> GetUserWithLocationsAsync(long userId, CancellationToken cancellationToken = default);
}
