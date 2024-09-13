using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Bot.TgTypes;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Services;

public class TelegramMessageSender : IMessageSender
{
    private readonly ITelegramBotClient _botClient;
    private readonly TelegramFileAdapter _fileAdapter;

    public TelegramMessageSender(
        ITelegramBotClient botClient,
        TelegramFileAdapter fileAdapter)
    {
        _botClient = botClient;
        _fileAdapter = fileAdapter;
    }

    public async Task SendTextMessageAsync(
        long chatId,
        string text,
        CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            cancellationToken: cancellationToken);
    }

    public async Task SendTextMessageAsync(
        long chatId,
        string text,
        IAppReplyMarkup replyMarkup,
        CancellationToken cancellationToken)
    {
        IReplyMarkup? telegramReplyMarkup = replyMarkup switch
        {
            AppKeyboardMarkup km => km.TelegramReplyKeyboardMarkup,
            RemoveKeyboardMarkup rkm => rkm.ReplyKeyboardRemove,
            _ => new ReplyKeyboardRemove()
        };

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            replyMarkup: telegramReplyMarkup,
            cancellationToken: cancellationToken);
    }

    public async Task SendLocationRequestAsync(
        long chatId,
        string messageText,
        string buttonText,
        string[] additionalButtons,
        CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: messageText,
            replyMarkup: GetKeyboard(buttonText, additionalButtons),
            cancellationToken: cancellationToken);
    }

    public async Task SendPhotoAsync(
        long chatId,
        IFile photo,
        CancellationToken cancellationToken)
    {
        await _botClient.SendPhotoAsync(
            chatId: chatId,
            photo: _fileAdapter.ConvertToTelegramFile(photo),
            cancellationToken: cancellationToken);
    }

    private ReplyKeyboardMarkup GetKeyboard(
        string button,
        string[] additionalButtons)
    {
        List<KeyboardButton> buttons = [];
        buttons.Add(KeyboardButton.WithRequestLocation(button));

        foreach (var btn in additionalButtons)
        {
            buttons.Add(btn);
        }

        return new ReplyKeyboardMarkup(buttons);
    }
}
