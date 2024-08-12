using Application.Abstract;
using Application.Features.Locations.LocationRequest;
using Application.Messaging;
using Domain.Users;

namespace Application.Services.Commands.Strategy;

public class LocationRequestStrategy : ICommandStrategy
{
    public int Priority => 2;
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
