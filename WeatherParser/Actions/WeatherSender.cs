using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherParser.Models.Interfaces;
using WeatherParser.Services;
using User = WeatherParser.Models.User;

namespace WeatherParser.Actions;

public class WeatherSender(
    ITelegramBotClient telegramBotClient,
    IWeatherService weatherService,
    IWeatherValidator weatherValidator,
    ILocationRequester locationRequester)
    : IWeatherSender
{
    private readonly ITelegramBotClient _telegramBotClient = telegramBotClient;
    private readonly IWeatherService _weatherService = weatherService;
    private readonly IWeatherValidator _weatherValidator = weatherValidator;
    private readonly ILocationRequester _locationRequester = locationRequester;

    public async Task<Message> SendWeatherAsync(User user, CancellationToken cancellationToken)
    {
        if (!user.HasLocation())
        {
            return await _locationRequester.RequestLocation(user.Id, cancellationToken);
        }

        var weather = _weatherService.GetWeather(user.Latitude, user.Longitude);

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
