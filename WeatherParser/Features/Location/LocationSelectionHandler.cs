using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherParser.Bot.Constants;
using WeatherParser.Features.Location;

namespace WeatherParser.Bot.Features.Location;

public class LocationSelectionHandler : ILocationSelectionHandler
{
    private readonly ITelegramBotClient _botClient;

    public LocationSelectionHandler(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task<Message> SendLocationSelectionMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var replyMarkup = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Send geolocation", CallbackDatas.Geolocation),
                InlineKeyboardButton.WithCallbackData("Send location name", CallbackDatas.ByLocationName)
            },
        });

        return await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Select the method",
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);
    }
}
