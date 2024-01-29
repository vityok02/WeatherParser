using Telegram.Bot.Types;

namespace WeatherParser.Features.UserState.UserStateHandlers;

public interface IUserStateHandler
{
    Task<Message> HandleAsync(long userId, string text, CancellationToken cancellationToken = default);
}
