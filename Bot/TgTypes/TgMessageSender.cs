using Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.TgTypes;
internal class TgMessageSender : IMessageSender
{
    private readonly ITelegramBotClient _botClient;

    public TgMessageSender(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SendPhotoAsync(long chatId, IAppFile photo, CancellationToken cancellationToken)
    {
        await _botClient.SendPhotoAsync(
            chatId: chatId,
            photo: (TgFile)photo,
            cancellationToken: cancellationToken);
    }

    public async Task SendTextMessageAsync(
        long chatId, string text, IApplicationReplyMarkup replyMarkup, CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            replyMarkup: (IReplyMarkup)replyMarkup,
            cancellationToken: cancellationToken);
    }

    public async Task SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            cancellationToken: cancellationToken);
    }
}
