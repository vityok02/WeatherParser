using Application.Abstract;
using Application.Interfaces;
using Domain.Abstract;

namespace Application.Locations.SetLocationFromRequest;

internal sealed class SetLocationFromRequestCommandHandler : ICommandHandler<SetLocationFromRequestCommand>
{
    private readonly IMessageSender _messageSender;

    public SetLocationFromRequestCommandHandler(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task<Result> Handle(SetLocationFromRequestCommand command, CancellationToken cancellationToken)
    {
        // TODO:
        await _messageSender.SendTextMessageAsync(
            command.UserId,
            "Location successfully set",
            cancellationToken);

        return Result.Success();
        //throw new NotImplementedException();
    }
}
