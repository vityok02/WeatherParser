using Application.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Default;

public record DefaultCommandHandler : ICommandHandler<DefaultCommand>
{
    private readonly ITelegramBotClient _botClient;

    public DefaultCommandHandler(ITelegramBotClient telegramBotClient)
    {
        _botClient = telegramBotClient;
    }

    public async Task<Message> Handle(DefaultCommand command, CancellationToken cancellationToken)
    {
        var usageText = GetUsageText();

        return await _botClient.SendTextMessageAsync(
            chatId: command.UserId,
            text: usageText,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }

    private static string GetUsageText()
    {
        return "Usage:\n" +
            "/weather     - get weather information\n" +
            "/location    - send location\n";
    }
}
