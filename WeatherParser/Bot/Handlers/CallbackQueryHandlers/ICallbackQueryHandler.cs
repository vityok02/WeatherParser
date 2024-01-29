using Telegram.Bot.Types;

namespace WeatherParser.Bot.Bot.Handlers.CallbackQueryHandlers;

public interface ICallbackQueryHandler
{
    Task<Message> HandleAsync(long userId, CancellationToken cancellationToken = default);
}
