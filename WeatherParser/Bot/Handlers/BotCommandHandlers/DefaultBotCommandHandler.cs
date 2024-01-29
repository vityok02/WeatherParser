using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherParser.Bot.Bot.Handlers.BotCommandHandlers;

public class DefaultBotCommandHandler : IBotCommandHandler
{
    private readonly ITelegramBotClient _botClient;

    public DefaultBotCommandHandler(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task<Message> HandleAsync(Message message, CancellationToken cancellationToken = default)
    {
        return await SendUsageAsync(message, cancellationToken);
    }

    private async Task<Message> SendUsageAsync(Message message, CancellationToken cancellationToken = default)
    {
        var usageMessage = GetUsageMessage();

        return await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: usageMessage,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }

    private static string GetUsageMessage()
    {
        return "Usage:\n" +
            "/weather     - get weather information\n" +
            "/location    - send location\n";
    }
}