using WeatherParser.Models.GeocodingRecords;

namespace WeatherParser.Models.Interfaces;

public interface IUserService
{
    Task<User?> GetUserAsync(long id, CancellationToken cancellationToken = default);
    Task<string> GetUserLocationNameAsync(long userId, CancellationToken cancellationToken = default);
    Task AddUserToDbAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> IsUserInDbAsync(long id, CancellationToken cancellationToken = default);
    Task SetUserLocationAsync(long userId, Location location, CancellationToken cancellationToken = default);
}
