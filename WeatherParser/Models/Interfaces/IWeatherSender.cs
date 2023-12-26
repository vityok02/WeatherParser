using Telegram.Bot.Types;
using WeatherParser.Services;

namespace WeatherParser.Models.Interfaces;

public interface IWeatherSender : ISender
{
    Task<Message> SendWeatherAsync(User user, CancellationToken cancellationToken);
}
