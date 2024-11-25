using Application.Commands.Default;
using Application.Commands.Languages;
using Application.Commands.Locations.SetLocation;
using Application.Commands.Requests;
using Application.Commands.Requests.RequestPlaceName;
using Application.Commands.Weathers.Commands.SendForecastToday;
using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Translations;
using Common.Constants;
using Domain.Locations;
using MediatR;

namespace Application.Services.Bot.Strategies;

public class UserStateStrategy : ICommandStrategy
{
    private readonly IUserService _userService;
    private readonly ISessionManager _sessionManager;
    private readonly ISender _sender;
    private readonly IUserTranslationService _translationService;

    public UserStateStrategy(
        IUserService userService,
        ISessionManager sessionManager,
        ISender sender,
        IUserTranslationService translationService)
    {
        _userService = userService;
        _sessionManager = sessionManager;
        _sender = sender;
        _translationService = translationService;
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
            await _sender.Send(new RequestDayCommand(message.UserId, message.Text), cancelToken);
            userState = userSession.Get<UserState>("state");
        }

        var date = userSession.Get<DateTime>("date");
        var command = await GetUserStateCommand(message, coordinates!, userState.Value, date);
        return command;

    }

    private async Task<ICommand> GetUserStateCommand(
        IMessage message, Coordinates coordinates, UserState userState, DateTime date)
    {
        return userState switch
        {
            UserState.SetLocation =>
                new SetLocationCommand(message.UserId, message.Text),
            UserState.EnterLocation =>
                await GetRequestPlaceNameCommand(message.UserId, message.Text),
            UserState.GetDailyForecast =>
                new SendDailyForecastCommand(message.UserId, coordinates, date),
            UserState.ChangeLanguage =>
                new SetLanguageCommand(message.UserId, message.Text),
            _ => null!
        };
    }

    private async Task<ICommand> GetRequestPlaceNameCommand(long userId, string text)
    {
        var translation = await _translationService
            .GetUserTranslationAsync(userId, CancellationToken.None);

        if (text == translation.Buttons["Cancel"])
        {
            return new DefaultCommand(userId);
        }

        return new RequestPlaceNameCommand(userId, text);
    }
}
