using Application.Common.Abstract;
using Application.Commands.Locations.LocationRequest;
using Application.Messaging;
using Domain.Users;

namespace Application.Services.Commands.Strategies;

public class LocationRequestStrategy : ICommandStrategy
{
    private readonly IUserRepository _userRepository;

    public LocationRequestStrategy(IUserRepository repository)
    {
        _userRepository = repository;
    }

    public async Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancellationToken)
    {
        if (!await _userRepository
            .HasLocationAsync(message.UserId, cancellationToken))
        {
            return new LocationRequestCommand(message.UserId);
        }

        return null!;
    }
}
