using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Common.Constants;
using Domain.Abstract;

namespace Application.Commands.Default;

internal sealed class DefaultCommandHandler
    : ICommandHandler<DefaultCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IKeyboardMarkupGenerator _keyboardGenerator;

    public DefaultCommandHandler(
        IMessageSender messageSender,
        IKeyboardMarkupGenerator keyboardGenerator)
    {
        _messageSender = messageSender;
        _keyboardGenerator = keyboardGenerator;
    }

    public async Task<Result> Handle(
        DefaultCommand command, 
        CancellationToken cancellationToken)
    {
        IAppReplyMarkup keyboard = GetKeyboard();

        await _messageSender.SendTextMessageAsync(
            chatId: command.ChatId,
            text: "Select action",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken);

        return Result.Success();
    }

    private IAppReplyMarkup GetKeyboard()
    {
        IEnumerable<IEnumerable<string>> buttons = [
            [BotCommand.WeatherNow, BotCommand.ForecastToday],
            [BotCommand.ForecastTomorrow, BotCommand.Location]
            //[$"☀️{BotCommand.WeatherNow}", $"🌤️{BotCommand.ForecastToday}", $"🌥️{BotCommand.ForecastTomorrow}"],
            //[$"📍{BotCommand.Location}" , $"🌐{BotCommand.ChangeLanguage}"]
        ];

        var keyboard = _keyboardGenerator.BuildKeyboard(buttons);
        return keyboard;
    }
}
