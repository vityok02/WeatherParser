using Telegram.Bot.Types;

namespace WeatherParser.Handlers.CallbackQueryHandlers;

public interface ICallbackQueryHandler
{
    Task<Message> HandleAsync(long userId, CancellationToken cancellationToken = default);
}
