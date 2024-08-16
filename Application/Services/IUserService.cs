using Domain.Locations;

namespace Application.Services;

public interface IUserService
{
    Task<Coordinates?> GetUserCoordinatesAsync(long userId, CancellationToken cancellationToken);
}