using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherParser.Models.Constants;
using WeatherParser.Models.Interfaces;

namespace WeatherParser.Actions;

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
