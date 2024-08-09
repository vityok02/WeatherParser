using Application.Interfaces.ReplyMarkup;

namespace Application.Interfaces;

public interface IMessageSender
{
    Task SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken);
    Task SendTextMessageAsync(long chatId, string text, IAppReplyMarkup replyMarkup, CancellationToken cancellationToken);
    Task SendPhotoAsync(long chatId, IFile photo, CancellationToken cancellationToken);
}
