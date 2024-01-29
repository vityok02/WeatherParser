using Telegram.Bot.Types;

namespace WeatherParser.Models.Interfaces;

public interface ILocationSelectionHandler
{
    Task<Message> SendLocationSelectionMessageAsync(Message message, CancellationToken cancellationToken);
}
