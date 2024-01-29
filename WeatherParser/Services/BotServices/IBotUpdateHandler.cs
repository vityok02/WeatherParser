using Telegram.Bot.Types;

namespace WeatherParser.Services.BotServices;

public interface IBotUpdateHandler
{
    Task HandleAsync(Update update, CancellationToken cancellationToken);
}
