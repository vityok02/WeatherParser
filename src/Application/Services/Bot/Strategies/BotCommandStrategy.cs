using Application.Commands.Locations.ViewLocation;
using Application.Commands.Requests.RequestLanguage;
using Application.Commands.Requests.RequestLocation;
using Application.Commands.Weathers.Commands.SendForecastToday;
using Application.Commands.Weathers.Commands.SendWeatherNow;
using Application.Common.Abstract;
using Application.Common.Constants;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Translations;
using Domain.Locations;
using Domain.Users;

namespace Application.Services.Bot.Strategies;

public class BotCommandStrategy : ICommandStrategy
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionManager _sessionManager;
    private readonly IUserTranslationService _translation;

    public BotCommandStrategy(
        IUserRepository userRepository,
        ISessionManager sessionManager,
        IUserTranslationService userTranslationService)
    {
        _userRepository = userRepository;
        _sessionManager = sessionManager;
        _translation = userTranslationService;
    }

    public async Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdWithLocationsAsync(message.UserId, cancellationToken);

        var userCoordinates = user!.CurrentLocation?.Coordinates;

        var command = await
            GetBotCommand(message, userCoordinates!, cancellationToken);

        return command;
    }

    private async Task<ICommand> GetBotCommand(
        IMessage message, Coordinates userCoordinates, CancellationToken token)
    {
        var translation = await _translation
            .GetUserTranslationAsync(message.UserId, token);

        var userId = message.UserId;

        var commandMappings = new Dictionary<string, Func<ICommand>>
        {
            {translation.Buttons[Buttons.ChangeLocation],
                () => new RequestLocationCommand(userId) },

            {translation.Buttons[Buttons.CurrentWeather],
                () => new SendWeatherNowCommand(userId, userCoordinates) },

            {translation.Buttons[Buttons.ForecastToday],
                () => new SendDailyForecastCommand(userId, userCoordinates, DateTime.Now) },

            {translation.Buttons[Buttons.ForecastTomorrow],
                () => new SendDailyForecastCommand(userId, userCoordinates, DateTime.Now.AddDays(1)) },

            {translation.Buttons[Buttons.ChangeLanguage],
                () => new RequestLanguageCommand(userId) },

            {translation.Buttons[Buttons.ViewLocation],
                () => new ViewLocationCommand(userId)}
        };

        foreach (var button in commandMappings.Keys)
        {
            if (message.Text.Contains(button, StringComparison.OrdinalIgnoreCase))
            {
                return commandMappings[button]();
            }
        }

        return null!;
    }
}