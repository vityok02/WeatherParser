using Application.Abstract;
using Application.Interfaces;
using Application.Messaging;
using Bot.TgTypes;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Services;
internal class TelegramMessageSender : IMessageSender
{
    private readonly ITelegramBotClient _botClient;
    private readonly TelegramFileAdapter _fileAdapter;

    public TelegramMessageSender(ITelegramBotClient botClient, TelegramFileAdapter fileAdapter)
    {
        _botClient = botClient;
        _fileAdapter = fileAdapter;
    }

    public async Task SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            cancellationToken: cancellationToken);
    }

    public async Task SendTextMessageAsync(
    long chatId, string text, IAppReplyMarkup replyMarkup, CancellationToken cancellationToken)
    {
        IReplyMarkup? telegramReplyMarkup = replyMarkup switch
        {
            KeyboardMarkup km => km.TelegramReplyKeyboardMarkup,
            RemoveKeyboardMarkup rkm => rkm.ReplyKeyboardRemove,
            _ => new ReplyKeyboardRemove()
        };

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            replyMarkup: telegramReplyMarkup,
            cancellationToken: cancellationToken);
    }

    public async Task SendLocationRequestAsync(long chatId, string messageText, string buttonText, CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: messageText,
            replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestLocation(buttonText)),
            cancellationToken: cancellationToken);
    }

    public async Task SendPhotoAsync(long chatId, IFile photo, CancellationToken cancellationToken)
    {
        await _botClient.SendPhotoAsync(
            chatId: chatId,
            photo: _fileAdapter.ConvertToTelegramFile(photo),
            cancellationToken: cancellationToken);
    }
}
