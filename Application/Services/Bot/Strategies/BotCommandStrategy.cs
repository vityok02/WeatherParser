using Application.Commands.Locations.LocationRequest;
using Application.Commands.Weathers.SendForecastToday;
using Application.Commands.Weathers.SendWeatherNow;
using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Common.Constants;
using Domain.Locations;
using Domain.Users;

namespace Application.Services.Bot.Strategies;

public class BotCommandStrategy : ICommandStrategy
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionManager _sessionManager;

    public BotCommandStrategy(IUserRepository userRepository, ISessionManager sessionManager)
    {
        _userRepository = userRepository;
        _sessionManager = sessionManager;
    }

    public async Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdWithLocationsAsync(message.UserId, cancellationToken);

        var userCoordinates = user!.CurrentLocation?.Coordinates;

        var text = message.Text.Substring(1);

        var command =
            GetBotCommand(text, message.UserId, userCoordinates!);

        return command;
    }

    private static ICommand GetBotCommand(
        string text, long userId, Coordinates userCoordinates)
    {
        return text switch
        {
            BotCommand.Location =>
                new LocationRequestCommand(userId),
            BotCommand.WeatherNow =>
                new SendWeatherNowCommand(userId, userCoordinates!),
            BotCommand.ForecastToday =>
                new SendDailyForecastCommand(userId, userCoordinates, DateTime.Now),
            BotCommand.ForecastTomorrow =>
                new SendDailyForecastCommand(userId, userCoordinates, DateTime.Now.AddDays(1)),
            _ => null!
        };
    }
}