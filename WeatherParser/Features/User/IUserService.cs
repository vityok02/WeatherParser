namespace WeatherParser.Features.User;

public interface IUserService
{
    Task<Models.User?> GetUserAsync(long id, CancellationToken cancellationToken = default);
    Task<string> GetUserLocationNameAsync(long userId, CancellationToken cancellationToken = default);
    Task AddUserToDbAsync(Models.User user, CancellationToken cancellationToken = default);
    Task<bool> IsUserInDbAsync(long id, CancellationToken cancellationToken = default);
    Task SetUserLocationAsync(long userId, Models.Location location, CancellationToken cancellationToken = default);
}
