using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Abstract;

namespace Application.Commands.Default;

internal sealed class DefaultCommandHandler
    : ICommandHandler<DefaultCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IUserTranslationService _translationService;
    private readonly ISessionManager _sessionManager;
    private readonly IDefaultKeyboardFactory _keyboardFactory;

    public DefaultCommandHandler(
        IMessageSender messageSender,
        IUserTranslationService translations,
        ISessionManager sessionManager,
        IDefaultKeyboardFactory keyboardFactory)
    {
        _messageSender = messageSender;
        _translationService = translations;
        _sessionManager = sessionManager;
        _keyboardFactory = keyboardFactory;
    }

    public async Task<Result> Handle(
        DefaultCommand command,
        CancellationToken cancellationToken)
    {
        var translation = await _translationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        var userSession = _sessionManager.GetOrCreateSession(command.UserId);
        userSession.Remove("state");

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: translation.Messages["Start"],
            replyMarkup: _keyboardFactory
                .CreateKeyboard(translation),
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
