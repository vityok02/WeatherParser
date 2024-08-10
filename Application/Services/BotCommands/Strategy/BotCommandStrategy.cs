using Application.Abstract;
using Application.Features.Locations.LocationRequest;
using Application.Features.Weathers.SendForecastToday;
using Application.Features.Weathers.SendWeatherNow;
using Application.Messaging;
using Common.Constants;
using Domain.Locations;
using Domain.Users;

namespace Application.Services.Commands.Strategy;

public class BotCommandStrategy : ICommandStrategy
{
    private readonly IUserRepository _userRepository;

    public BotCommandStrategy(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdWithLocationsAsync(message.UserId, cancellationToken);

        var userCoordinates = user!.CurrentLocation!.Coordinates;

        var command =
            GetBotCommand(message.MessageText, message.UserId, userCoordinates);

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
                new SendForecastTodayCommand(userId, userCoordinates!),
            _ => null!
        };
    }
}