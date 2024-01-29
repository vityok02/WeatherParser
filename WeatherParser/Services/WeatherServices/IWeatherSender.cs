using Telegram.Bot.Types;
using WeatherParser.Models.Interfaces;

namespace WeatherParser.Services.WeatherServices;

public interface IWeatherSender : ISender
{
    Task<Message> SendWeatherAsync(long userId, CancellationToken cancellationToken = default);
}
