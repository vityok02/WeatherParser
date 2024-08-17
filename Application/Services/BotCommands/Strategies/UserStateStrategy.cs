using Application.Commands.Locations.EnterPlaceName;
using Application.Commands.Locations.SetLocation;
using Application.Commands.Weathers.EnterDay;
using Application.Commands.Weathers.SendForecastToday;
using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Messaging;
using Common.Constants;
using Domain.Locations;
using MediatR;

namespace Application.Services.Commands.Strategies;

public class UserStateStrategy : ICommandStrategy
{
    private readonly IUserService _userService;
    private readonly ISessionManager _sessionManager;
    private readonly ISender _sender;

    public UserStateStrategy(
        IUserService userService,
        ISessionManager sessionManager,
        ISender sender)
    {
        _userService = userService;
        _sessionManager = sessionManager;
        _sender = sender;
    }

    public async Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancelToken)
    {
        var userSession = _sessionManager
            .GetOrCreateSession(message.UserId);

        UserState? userState = userSession.Get<UserState>("state");

        var coordinates = await _userService
            .GetUserCoordinatesAsync(message.UserId, cancelToken);

        if (!userState.HasValue)
        {
            return null!;
        }

        // TODO: refactor logic with Sender.
        if (userState.Value == UserState.EnterDay)
        {
            await _sender.Send(new EnterDayCommand(message.UserId, message.Text), cancelToken);
            userState = userSession.Get<UserState>("state");
        }

        var date = userSession.Get<DateTime>("date");
        var command = GetUserStateCommand(message, coordinates!, userState.Value, date);
        return command;

    }

    private ICommand GetUserStateCommand(
        IMessage message, Coordinates coordinates, UserState userState, DateTime date)
    {
        return userState switch
        {
            UserState.SetLocation =>
                new SetLocationCommand(message.UserId, message.Text),
            UserState.EnterLocation =>
                new EnterPlaceNameCommand(message.UserId, message.Text),
            UserState.GetDailyForecast =>
                new SendDailyForecastCommand(message.UserId, coordinates, date),
            _ => null!
        };
    }
}
