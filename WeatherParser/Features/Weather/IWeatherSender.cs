using Telegram.Bot.Types;

namespace WeatherParser.Features.Weather;

public interface IWeatherSender
{
    Task<Message> SendWeatherAsync(long userId, CancellationToken cancellationToken = default);
}
