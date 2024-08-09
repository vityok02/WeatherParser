using Application.Abstract;
using Application.Interfaces;
using Domain.Abstract;
using Microsoft.Extensions.Logging;

namespace Application.Locations.SetLocationFromRequest;

internal sealed class SetLocationFromRequestCommandHandler : ICommandHandler<SetLocationFromRequestCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ILogger<SetLocationFromRequestCommandHandler> _logger;

    public SetLocationFromRequestCommandHandler(IMessageSender messageSender, ILogger<SetLocationFromRequestCommandHandler> logger)
    {
        _messageSender = messageSender;
        _logger = logger;
    }

    public async Task<Result> Handle(SetLocationFromRequestCommand command, CancellationToken cancellationToken)
    {
        // TODO:
        await _messageSender.SendTextMessageAsync(
            command.UserId,
            "The feature is in development",
            cancellationToken);

        _logger.LogError("Feture does not work");

        return Result.Success();
        //throw new NotImplementedException();
    }
}
