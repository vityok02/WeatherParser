using Bot.Constants;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Features.Locations;

public sealed class LocationMethodSelector
{
    private readonly ITelegramBotClient _botClient;

    public LocationMethodSelector(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SendChoice(long chatId, CancellationToken cancellationToken)
    {
        var replyMarkup = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Send geolocation", CallbackData.SendGeolocationRequest),
                InlineKeyboardButton.WithCallbackData("Enter place name", CallbackData.SendPlaceName)
            },
        });

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Select the method",
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);
    }
}
