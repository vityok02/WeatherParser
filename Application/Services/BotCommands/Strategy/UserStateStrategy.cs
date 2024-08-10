using Application.Abstract;
using Application.Features.Locations.SetLocation;
using Application.Interfaces;
using Application.Messaging;
using Common.Constants;

namespace Application.Services.Commands.Strategy;

public class UserStateStrategy : ICommandStrategy
{
    private readonly IUserStateRepository _userStateRepository;

    public UserStateStrategy(IUserStateRepository repository)
    {
        _userStateRepository = repository;
    }

    public Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancelToken)
    {
        var userState = _userStateRepository
            .GetState(message.UserId);

        if (userState.HasValue)
        {
            var command = GetUserStateCommand(
                message.MessageText, message.UserId, userState.Value);

            return Task
                .FromResult(command);
        }

        return Task
            .FromResult<ICommand>(null!);
    }

    private static ICommand GetUserStateCommand(
        string messageText, long userId, UserState userState)
    {
        return userState switch
        {
            UserState.SetLocation =>
                new SetLocationCommand(userId, messageText),
            _ => null!
        };
    }
}
