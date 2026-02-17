using Domain.Locations;
using Domain.Users;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Coordinates?> GetUserCoordinatesAsync(long userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdWithLocationsAsync(userId, cancellationToken);
        return user?.CurrentLocation?.Coordinates;
    }
}
