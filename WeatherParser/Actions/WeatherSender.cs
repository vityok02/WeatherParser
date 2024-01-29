using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherParser.Data;
using WeatherParser.Models;
using WeatherParser.Models.Interfaces;
using WeatherParser.Services.WeatherServices;

namespace WeatherParser.Actions;

public class WeatherSender(
    ITelegramBotClient telegramBotClient,
    AppDbContext dbContext,
    IWeatherService weatherService,
    IValidator<Weather> weatherValidator,
    ILocationRequester locationRequester)
    : IWeatherSender
{
    private readonly ITelegramBotClient _telegramBotClient = telegramBotClient;
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IWeatherService _weatherService = weatherService;
    private readonly IValidator<Weather> _weatherValidator = weatherValidator;
    private readonly ILocationRequester _locationRequester = locationRequester;

    public async Task<Message> SendWeatherAsync(long userId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == userId)
            .Include(u => u.CurrentLocation)
            .FirstOrDefaultAsync(cancellationToken);

        if (!user!.HasLocation)
        {
            return await _locationRequester.RequestLocationAsync(user.Id, cancellationToken);
        }

        var coordinates = new Coordinates(user.CurrentLocation!.Latitude, user.CurrentLocation.Longitude);

        var weather = _weatherService.GetWeather(coordinates);

        var validationResult = _weatherValidator.Validate(weather);

        if (!validationResult.IsValid)
        {
            return await _telegramBotClient.SendTextMessageAsync(
                chatId: user.Id,
                text: validationResult.ToString(),
                cancellationToken: cancellationToken);
        }

        return await _telegramBotClient.SendTextMessageAsync(
            chatId: user.Id,
            text: weather.ToString(),
            cancellationToken: cancellationToken);
    }
}
