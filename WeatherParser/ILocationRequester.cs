using Telegram.Bot.Types;

namespace WeatherParser;

public interface ILocationRequester
{
    Task<Message> RequestLocationAsync(long userId, CancellationToken cancellationToken = default);
}
