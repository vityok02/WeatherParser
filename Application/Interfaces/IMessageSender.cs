using Telegram.Bot.Types;

namespace Application.Interfaces;

public interface IMessageSender
{
    Task SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken);
    Task SendTextMessageAsync(long chatId, string text, IApplicationReplyMarkup replyMarkup, CancellationToken cancellationToken);
    Task SendPhotoAsync(long chatId, IAppFile photo, CancellationToken cancellationToken);
}
