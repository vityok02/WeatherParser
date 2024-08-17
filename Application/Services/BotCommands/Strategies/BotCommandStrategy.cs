using Application.Common.Abstract;
using Application.Commands.Locations.LocationRequest;
using Application.Commands.Weathers.EnterDay;
using Application.Commands.Weathers.SendMultiDayForecast;
using Application.Commands.Weathers.SendWeatherNow;
using Application.Messaging;
using Common.Constants;
using Domain.Locations;
using Domain.Users;
using Application.Commands.Weathers.SelectDay;
using Application.Common.Interfaces;

namespace Application.Services.Commands.Strategies;

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

        var session = _sessionManager.GetOrCreateSession(message.UserId);

        var userCoordinates = user!.CurrentLocation?.Coordinates;

        var command =
            GetBotCommand(message.Text, message.UserId, userCoordinates);

        return command;
    }

    private static ICommand GetBotCommand(
        string text, long userId, Coordinates? userCoordinates)
    {
        return text.Split(' ').First() switch
        {
            BotCommand.Location =>
                new LocationRequestCommand(userId),
            BotCommand.WeatherNow =>
                new SendWeatherNowCommand(userId, userCoordinates!),
            BotCommand.ForecastToday =>
                new SelectDayCommand(userId, text),
            BotCommand.MultiDayForecast =>
                new SendMultidayForecastCommand(userId, ParseDays(text), userCoordinates!),
            _ => null!
        };
    }

    private static DateTime ParseDate(string text)
    {
        DateTime.TryParse(text, out var date);

        return Convert.ToDateTime(date);
    }

    private static int ParseDays(string text)
    {
        int.TryParse(text, out var days);

        return days;
    }
}