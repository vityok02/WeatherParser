using Application.Commands.Requests.RequestLocation;
using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Users;

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
            return new RequestLocationCommand(message.UserId);
        }

        return null!;
    }
}
