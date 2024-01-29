using Telegram.Bot.Types;

namespace WeatherParser.Bot;

public interface IBotUpdateHandler
{
    Task HandleAsync(Update update, CancellationToken cancellationToken);
}
