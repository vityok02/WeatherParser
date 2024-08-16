using Application.Abstract;
using Application.Messaging;
using Domain.Abstract;

namespace Application.Features.Weathers.SelectDay;

public sealed record SelectDayCommand(long ChatId, string Day) : ICommand;

internal sealed class SelectDayCommandHandler : ICommandHandler<SelectDayCommand>
{
    private readonly IMessageSender _messageSender;

    public SelectDayCommandHandler(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task<Result> Handle(SelectDayCommand command, CancellationToken cancellationToken)
    {
        await _messageSender.SendKeyboardAsync(
            command.ChatId,
            "t",
            cancellationToken);

        return Result.Success();
    }
}
