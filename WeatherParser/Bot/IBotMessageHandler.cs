using Telegram.Bot.Types;

namespace WeatherParser.Bot;

public interface IBotMessageHandler
{
    Task HandleMessage(Message message, CancellationToken cancellationToken);
    Task HandleCallbackQuery(CallbackQuery callbackQuery, CancellationToken cancellationToken);
    Task UnknownUpdateHandlerAsync(Update update);
}
