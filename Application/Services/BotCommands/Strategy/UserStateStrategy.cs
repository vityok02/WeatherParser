using Application.Abstract;
using Application.Features.Locations.EnterPlaceName;
using Application.Features.Locations.SetLocation;
using Application.Features.Weathers.SendForecastToday;
using Application.Interfaces;
using Application.Messaging;
using Common.Constants;
using Domain.Locations;

namespace Application.Services.Commands.Strategy;

public class UserStateStrategy : ICommandStrategy
{
    private readonly IUserStateRepository _userStateRepository;
    private readonly IUserService _userService;

    public UserStateStrategy(
        IUserStateRepository repository, 
        IUserService userService)
    {
        _userStateRepository = repository;
        _userService = userService;
    }

    public async Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancelToken)
    {
        var userState = _userStateRepository
            .GetState(message.UserId);

        var coordinates = await _userService
            .GetUserCoordinatesAsync(message.UserId, cancelToken);

        if (userState.HasValue)
        {
            var command = GetUserStateCommand(
                message.MessageText, message.UserId, userState.Value);

            return command;
        }

        return null!;
    }

    private static ICommand GetUserStateCommand(
        string messageText, long userId, Coordinates coordinates, UserState userState)
    {
        return userState switch
        {
            UserState.SetLocation =>
                new SetLocationCommand(userId, messageText),
            UserState.EnterLocation =>
                new EnterPlaceNameCommand(userId, messageText),
            UserState.GetDailyForecast =>
                new SendDailyForecastCommand(userId, coordinates, messageText),
            _ => null!
        };
    }
}
