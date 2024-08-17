using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Common.Constants;
using Domain.Abstract;

namespace Application.Commands.Weathers.SelectDay;

internal sealed class SelectDayCommandHandler
    : ICommandHandler<SelectDayCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ISessionManager _sessionManager;

    public SelectDayCommandHandler(
        IMessageSender messageSender, 
        ISessionManager sessionManager)
    {
        _messageSender = messageSender;
        _sessionManager = sessionManager;
    }

    public async Task<Result> Handle(
        SelectDayCommand command, 
        CancellationToken cancellationToken)
    {
        await _messageSender.SendTextMessageAsync(
            command.ChatId,
            "Select day",
            cancellationToken);

        var userSession = _sessionManager.GetOrCreateSession(command.ChatId);

        userSession.Set("state", UserState.EnterDay);

        return Result.Success();
    }
}
