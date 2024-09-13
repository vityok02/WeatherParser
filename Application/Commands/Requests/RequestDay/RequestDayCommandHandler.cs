using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Common.Constants;
using Domain.Abstract;

namespace Application.Commands.Requests;

internal sealed class RequestDayCommandHandler
    : ICommandHandler<RequestDayCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ISessionManager _sessionManager;
    private readonly IUserTranslationService _userTranslationService;

    public RequestDayCommandHandler(
        IMessageSender messageSender,
        ISessionManager sessionManager,
        IUserTranslationService userTranslationService)
    {
        _messageSender = messageSender;
        _sessionManager = sessionManager;
        _userTranslationService = userTranslationService;
    }

    public async Task<Result> Handle(
        RequestDayCommand command,
        CancellationToken cancellationToken)
    {
        var translation = await _userTranslationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        await _messageSender.SendTextMessageAsync(
            command.UserId,
            translation.Messages["RequestDay"],
            cancellationToken);

        var userSession = _sessionManager
            .GetOrCreateSession(command.UserId);
        userSession.Set("state", UserState.EnterDay);

        return Result.Success();
    }
}
