using Telegram.Bot.Types;

namespace Application.Interfaces;

public interface IBotUpdateHandler
{
    Task HandleAsync(Update update, CancellationToken cancellationToken);
}
