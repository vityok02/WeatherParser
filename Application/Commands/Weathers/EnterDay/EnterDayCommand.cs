using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Common.Constants;
using Domain.Abstract;

namespace Application.Commands.Weathers.EnterDay;

public record EnterDayCommand(long UserId, string Text) : ICommand;

internal class EnterDayCommandHandler
    : ICommandHandler<EnterDayCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ISessionManager _sessionManager;

    public EnterDayCommandHandler(
        IMessageSender messageSender,
        ISessionManager sessionManager)
    {
        _messageSender = messageSender;
        _sessionManager = sessionManager;
    }

    public async Task<Result> Handle(
        EnterDayCommand command, 
        CancellationToken cancellationToken)
    {
        var day = Days.Value.GetValueOrDefault(command.Text);

        if (day == DateTime.MinValue)
        {
            await _messageSender.SendTextMessageAsync(
                command.UserId,
                "Try again",
                cancellationToken);

            return Result.Failure("Day.NotValid", "Value from user is not valid");
        }

        var session = _sessionManager
            .GetOrCreateSession(command.UserId);

        session.Set("date", day);
        session.Set("state", UserState.GetDailyForecast);

        return Result.Success();
    }
}
