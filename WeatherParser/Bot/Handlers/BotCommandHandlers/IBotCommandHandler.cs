using Telegram.Bot.Types;

namespace WeatherParser.Bot.Bot.Handlers.BotCommandHandlers;

public interface IBotCommandHandler
{
    Task<Message> HandleAsync(Message message, CancellationToken cancellationToken = default);
}
