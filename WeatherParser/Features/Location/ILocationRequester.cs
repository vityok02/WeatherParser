using Telegram.Bot.Types;

namespace WeatherParser.Features.Location;

public interface ILocationRequester
{
    Task<Message> RequestLocationAsync(long userId, CancellationToken cancellationToken = default);
}
