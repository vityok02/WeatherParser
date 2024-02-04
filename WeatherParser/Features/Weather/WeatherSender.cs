using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherParser.Abstract;
using WeatherParser.Data;
using WeatherParser.Features.Location;
using WeatherParser.Features.Weather;
using WeatherParser.Models;

namespace WeatherParser.Bot.Features.Weather;

public class WeatherSender(
    ITelegramBotClient telegramBotClient,
    AppDbContext dbContext,
    IWeatherService weatherService,
    IValidator<Models.Weather> weatherValidator,
    IWeatherUrlGenerator _urlGenerator,
    ISelectLocationSender selectLocationSender)
    : IWeatherSender
{
    private readonly ITelegramBotClient _telegramBotClient = telegramBotClient;
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IWeatherService _weatherService = weatherService;
    private readonly IValidator<Models.Weather> _weatherValidator = weatherValidator;
    private readonly ISelectLocationSender _selectLocationSender = selectLocationSender;
    private readonly IWeatherUrlGenerator _urlGenerator = _urlGenerator;

    public async Task<Message> SendWeatherAsync(long userId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == userId)
            .Include(u => u.CurrentLocation)
            .FirstOrDefaultAsync(cancellationToken);

        if (!user!.HasLocation)
        {
            return await _selectLocationSender.SendSelectLocationMethodsAsync(user.Id, cancellationToken);
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

        var text = $"{weather}\n" +
            $"Learn more: {_urlGenerator.GenerateUrl(user.CurrentLocation.Latitude, user.CurrentLocation.Longitude)}";

        return await _telegramBotClient.SendTextMessageAsync(
            chatId: user.Id,
            text: text,
            cancellationToken: cancellationToken);
    }
}
