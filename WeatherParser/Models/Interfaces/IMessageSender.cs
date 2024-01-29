using Telegram.Bot.Types;

namespace WeatherParser.Models.Interfaces;

public interface IMessageSender
{
    Task<Message> SendMessageAsync(long chatId, string text, CancellationToken cancellationToken = default);
}
