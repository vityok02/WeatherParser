using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Common.Constants;
using Domain.Abstract;

namespace Application.Commands.Requests.RequestLocation;

internal sealed class RequestLocationCommandHandler
    : ICommandHandler<RequestLocationCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ISessionManager _sessionManager;
    private readonly IUserTranslationService _translationService;

    public RequestLocationCommandHandler(
        IMessageSender messageSender,
        ISessionManager sessionManager,
        IUserTranslationService userTranslationService)
    {
        _messageSender = messageSender;
        _sessionManager = sessionManager;
        _translationService = userTranslationService;
    }

    public async Task<Result> Handle(
        RequestLocationCommand command, CancellationToken cancellationToken)
    {
        var userSession = _sessionManager
            .GetOrCreateSession(command.UserId);
        userSession
            .Set("state", UserState.EnterLocation);

        var translation = await _translationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        await _messageSender.SendLocationRequestAsync(
            chatId: command.UserId,
            messageText: translation.Messages["RequestLocation"],
            buttonText: translation.Buttons["ShareLocation"],
            additionalButtons: [translation.Buttons["Cancel"]],
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
