using Application.Common.Abstract;

namespace Application.Common.Interfaces.Messaging;

public interface IMessageSender
{
    Task SendTextMessageAsync(
        long chatId, string text, CancellationToken cancellationToken);
    Task SendTextMessageAsync(
        long chatId, string text, IAppReplyMarkup replyMarkup, CancellationToken cancellationToken);
    Task SendLocationRequestAsync(
        long chatId, string messageText, string buttonText, string[] additionalButtons, CancellationToken cancellationToken);
    Task SendPhotoAsync(
        long chatId, IFile photo, CancellationToken cancellationToken);
}
