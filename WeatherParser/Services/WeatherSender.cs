using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherParser.Models.Interfaces;
using User = WeatherParser.Models.User;

namespace WeatherParser.Services;

public interface ISender
{
    Task<Message> SendAsync(User user, long chatId);
}

public interface IWeatherSender : ISender
{

}

public class WeatherSender : IWeatherSender
{
    private readonly IWeatherService _weatherService;
    private readonly IWeatherValidator _weatherValidator;
    private readonly ITelegramBotClient _telegramBotClient;

    public WeatherSender(IWeatherService weatherService,
        IWeatherValidator weatherValidator,
        ITelegramBotClient telegramBotClient)
    {
        _weatherService = weatherService;
        _weatherValidator = weatherValidator;
        _telegramBotClient = telegramBotClient;
    }

    public async Task<Message> SendAsync(User user, long chatId)
    {
        var weather = _weatherService?.GetWeather(user.Latitude, user.Longitude);

        var validationResult = _weatherValidator.Validate(weather);

        if (!validationResult.IsValid)
        {
            validationResult.ToString();
            return await _telegramBotClient.SendTextMessageAsync(
                chatId: chatId,
                text: validationResult.ToString());
        }

        weather!.ToString();

        return await _telegramBotClient.SendTextMessageAsync(
            chatId: chatId,
            text: weather!.ToString());
    }
}
