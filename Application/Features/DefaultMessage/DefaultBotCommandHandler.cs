using Application.Abstract;
using Domain.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Features.DefaultMessage;

public record DefaultBotCommandHandler : ICommandHandler<DefaultBotCommand>
{
    private readonly ITelegramBotClient _botClient;

    public DefaultBotCommandHandler(ITelegramBotClient telegramBotClient)
    {
        _botClient = telegramBotClient;
    }

    public async Task<Result> Handle(DefaultBotCommand command, CancellationToken cancellationToken)
    {
        var usageText = GetUsageText();

        await _botClient.SendTextMessageAsync(
            chatId: command.UserId,
            text: usageText,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        return Result.Success();
    }

    private static string GetUsageText()
    {
        return "Usage:\n" +
            "/weather     - get weather information\n" +
            "/location    - send location\n";
    }
}
