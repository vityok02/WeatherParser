using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.BotHandlers;

public record DefaultMessageHandler
{
    private readonly ITelegramBotClient _messageSender;

    public DefaultMessageHandler(ITelegramBotClient telegramBotClient)
    {
        _messageSender = telegramBotClient;
    }

    public async Task Handle(long chatId, CancellationToken cancellationToken)
    {
        var usageText = GetUsageText();

        await _messageSender.SendTextMessageAsync(
            chatId: chatId,
            text: usageText,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }

    private static string GetUsageText()
    {
        return "Usage:\n" +
            "/weather  - get weather information\n" +
            "/location - send location\n";
    }
}
