using Telegram.Bot.Types;

namespace WeatherParser.Services.BotServices;

public interface IBotCommandHandler
{
    Task<Message> HandleAsync(Message message, CancellationToken cancellationToken = default);
}
