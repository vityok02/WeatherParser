using Telegram.Bot.Types;

namespace Bot.Messages;

public interface IMessageHandler
{
    Task HandleMessage(Message message, CancellationToken cancellationToken);
}