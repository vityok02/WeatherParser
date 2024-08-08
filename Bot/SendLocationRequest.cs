using Application.Interfaces;
using Domain.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot;

public sealed class SendLocationRequest
{
    private readonly ITelegramBotClient _botClient;

    public SendLocationRequest(ITelegramBotClient botClient, IUserStateRepository cachedUserStateRepository)
    {
        _botClient = botClient;
    }

    public async Task Handle(long chatId, CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Click the button to send your geolocation",
            replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestLocation("Send geolocation")),
            cancellationToken: cancellationToken);
    }
}
