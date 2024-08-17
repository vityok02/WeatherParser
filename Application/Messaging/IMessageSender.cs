using Application.Common.Abstract;
using Application.Common.Interfaces;

namespace Application.Messaging;

public interface IMessageSender
{
    Task SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken);
    Task SendTextMessageAsync(long chatId, string text, IAppReplyMarkup replyMarkup, CancellationToken cancellationToken);
    Task SendLocationRequestAsync(long chatId, string messageText, string buttonText, CancellationToken cancellationToken);
    Task SendPhotoAsync(long chatId, IFile photo, CancellationToken cancellationToken);
    Task SendKeyboardAsync(long chatId, string messateText, CancellationToken cancellationToken);
}
