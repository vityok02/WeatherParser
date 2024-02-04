using Telegram.Bot.Types;

namespace WeatherParser.Features.Location;

public interface ISelectLocationSender
{
    Task<Message> SendSelectLocationMethodsAsync(long userId, CancellationToken cancellationToken);
}
