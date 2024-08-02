using Application.Abstract;
using Application.Interfaces;
using Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Features.CallbackQueryCommands.SendGeolocationRequestCommand;

internal class SendGeolocationRequestCommandHandler : ICommandHandler<SendGeolocationRequestCommand>
{
    private readonly ITelegramBotClient _botClient;

    public SendGeolocationRequestCommandHandler(ITelegramBotClient botClient, ICachedUserStateRepository cachedUserStateRepository)
    {
        _botClient = botClient;
    }

    public async Task<Result> Handle(SendGeolocationRequestCommand command, CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: command.UserId,
            text: "Click the button to send your geolocation",
            replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestLocation("Send geolocation")),
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
