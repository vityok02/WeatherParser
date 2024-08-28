using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Domain.Abstract;
using Domain.Users;

namespace Application.Commands.Default;

internal sealed class DefaultCommandHandler
    : ICommandHandler<DefaultCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IKeyboardMarkupGenerator _keyboardGenerator;
    private readonly ITranslationService _translations;
    private readonly ISessionManager _sessionManager;

    public DefaultCommandHandler(
        IMessageSender messageSender,
        IKeyboardMarkupGenerator keyboardGenerator,
        ITranslationService translations,
        ISessionManager sessionManager)
    {
        _messageSender = messageSender;
        _keyboardGenerator = keyboardGenerator;
        _translations = translations;
        _sessionManager = sessionManager;
    }

    public async Task<Result> Handle(
        DefaultCommand command, 
        CancellationToken cancellationToken)
    {
        var userSession = _sessionManager.GetOrCreateSession(command.UserId);
        userSession.Remove("state");

        await _translations.SetLanguageByUserId(command.UserId, cancellationToken);

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: _translations.StartMessage,
            replyMarkup: DefaultKeyboard
                .GetKeyboard(_keyboardGenerator),
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
