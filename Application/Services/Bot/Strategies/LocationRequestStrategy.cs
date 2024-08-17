using Application.Common.Abstract;
using Application.Commands.Locations.LocationRequest;
using Domain.Users;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces;

namespace Application.Services.Bot.Strategies;

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
