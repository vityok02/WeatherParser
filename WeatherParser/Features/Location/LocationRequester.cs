using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherParser.Features.Location;

public class LocationRequester : ILocationRequester
{
    private readonly ITelegramBotClient _botClient;

    public LocationRequester(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task<Message> RequestLocationAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: userId,
            text: "Click the button to send your geolocation",
            replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestLocation("Send geolocation")),
            cancellationToken: cancellationToken);
    }
}
