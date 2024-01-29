using Telegram.Bot.Types;

namespace WeatherParser.Bot.Features.MessageSender;

public interface IMessageSender
{
    Task<Message> SendMessageAsync(long chatId, string text, CancellationToken cancellationToken = default);
}
