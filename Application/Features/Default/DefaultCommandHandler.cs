using Application.Abstract;
using Application.Interfaces.ReplyMarkup;
using Application.Messaging;
using Domain.Abstract;

namespace Application.Features.Default;

internal sealed class DefaultCommandHandler : ICommandHandler<DefaultCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IRemoveKeyboardMarkup _replyMarkup;

    public DefaultCommandHandler(IMessageSender messageSender, IRemoveKeyboardMarkup replyMarkup)
    {
        _messageSender = messageSender;
        _replyMarkup = replyMarkup;
    }

    public async Task<Result> Handle(DefaultCommand command, CancellationToken cancellationToken)
    {
        var usageText = GetUsageText();

        await _messageSender.SendTextMessageAsync(
            chatId: command.ChatId,
            text: usageText,
            replyMarkup: _replyMarkup,
            cancellationToken: cancellationToken);

        return Result.Success();
    }

    private static string GetUsageText()
    {
        return "Usage:\n" +
            "/weather  - get weather information\n" +
            "/location - send location\n";
    }
}
