using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
namespace WeatherParser.Actions;

public interface ILocationRequester
{
    Task<Message> RequestLocation(long chatId, CancellationToken cancellationToken);
}

public class LocationRequester(ITelegramBotClient botClient) : ILocationRequester
{
    private readonly ITelegramBotClient _botClient = botClient;

    public async Task<Message> RequestLocation(long chatId, CancellationToken cancellationToken)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Where are you?",
            replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestLocation("Location")),
            cancellationToken: cancellationToken);
    }
}
