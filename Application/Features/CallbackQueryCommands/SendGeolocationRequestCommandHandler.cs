using Application.Abstract;
using Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Features.CallbackQueryCommands;

internal class SendGeolocationRequestCommandHandler : ICommandHandler<SendGeolocationRequestCommand>
{
    private readonly ITelegramBotClient _botClient;

    public SendGeolocationRequestCommandHandler(ITelegramBotClient botClient, IUserStateRepository cachedUserStateRepository)
    {
        _botClient = botClient;
    }

    public async Task<Message> Handle(SendGeolocationRequestCommand command, CancellationToken cancellationToken)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: command.UserId,
            text: "Click the button to send your geolocation",
            replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestLocation("Send geolocation")),
            cancellationToken: cancellationToken);
    }
}
