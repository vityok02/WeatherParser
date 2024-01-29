using Telegram.Bot.Types;

namespace WeatherParser.Bot.Bot.Handlers.BotHandlers;

public class BotUpdateHandler : IBotUpdateHandler
{
    public readonly IBotMessageHandler _botMessageHandler;

    public BotUpdateHandler(IBotMessageHandler botMessageHandler)
    {
        _botMessageHandler = botMessageHandler;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            { Message: { } message } => _botMessageHandler.HandleMessage(message, cancellationToken),
            { CallbackQuery: { } callbackQuery } => _botMessageHandler.HandleCallbackQuery(callbackQuery, cancellationToken),
            _ => _botMessageHandler.UnknownUpdateHandlerAsync(update)
        };

        await handler;
    }
}
