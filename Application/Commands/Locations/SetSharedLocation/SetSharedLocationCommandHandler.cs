using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Services;
using Domain.Abstract;
using Microsoft.Extensions.Logging;

namespace Application.Locations.SetLocationFromRequest;

internal sealed class SetSharedLocationCommandHandler
    : ICommandHandler<SetSharedLocationCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ILogger<SetSharedLocationCommandHandler> _logger;
    private readonly IGeocodingService _geocodingService;

    public SetSharedLocationCommandHandler(
        IMessageSender messageSender,
        ILogger<SetSharedLocationCommandHandler> logger)
    {
        _messageSender = messageSender;
        _logger = logger;
    }

    public async Task<Result> Handle(
        SetSharedLocationCommand command,
        CancellationToken cancellationToken)
    {
        // TODO:
        await _messageSender.SendTextMessageAsync(
            command.UserId,
            "The feature is in development",
            cancellationToken);

        return Result.Failure("Feature.NotWorking", "The feature is in development");
    }
}
