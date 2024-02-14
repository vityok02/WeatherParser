using Application.Abstract;
using Application.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Features.Locations;

internal sealed class LocationCommandHandler : ICommandHandler<LocationCommand>
{
    private readonly ITelegramBotClient _botClient;

    public LocationCommandHandler(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task<Message> Handle(LocationCommand command, CancellationToken cancellationToken)
    {
        var replyMarkup = new InlineKeyboardMarkup(new[]
{
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Send geolocation", CallbackData.RequestLocation),
                InlineKeyboardButton.WithCallbackData("Enter place name", CallbackData.EnterPlaceName)
            },
        });

        return await _botClient.SendTextMessageAsync(
            chatId: command.UserId,
            text: "Select the method",
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);
    }
}
