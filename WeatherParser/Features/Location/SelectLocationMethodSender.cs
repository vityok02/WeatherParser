using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherParser.Bot.Constants;
using WeatherParser.Features.Location;

namespace WeatherParser.Bot.Features.Location;

public class SelectLocationMethodSender : ISelectLocationSender
{
    private readonly ITelegramBotClient _botClient;

    public SelectLocationMethodSender(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task<Message> SendSelectLocationMethodsAsync(long userId, CancellationToken cancellationToken)
    {
        var replyMarkup = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Send geolocation", CallbackData.Geolocation),
                InlineKeyboardButton.WithCallbackData("Send location name", CallbackData.ByLocationName)
            },
        });

        return await _botClient.SendTextMessageAsync(
            chatId: userId,
            text: "Select the method",
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);
    }
}
