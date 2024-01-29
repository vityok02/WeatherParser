using Telegram.Bot.Types;

namespace WeatherParser.Features.Location;

public interface ILocationSelectionHandler
{
    Task<Message> SendLocationSelectionMessageAsync(Message message, CancellationToken cancellationToken);
}
