﻿using Application.Abstract;
using Application.Interfaces;
using Application.Messaging;
using Domain.Abstract;
using Microsoft.Extensions.Logging;

namespace Application.Locations.SetLocationFromRequest;

internal sealed class SetSharedLocationCommandHandler : ICommandHandler<SetSharedLocationCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ILogger<SetSharedLocationCommandHandler> _logger;
    private readonly IGeocodingService _geocodingService;

    public SetSharedLocationCommandHandler(IMessageSender messageSender, ILogger<SetSharedLocationCommandHandler> logger)
    {
        _messageSender = messageSender;
        _logger = logger;
    }

    public async Task<Result> Handle(SetSharedLocationCommand command, CancellationToken cancellationToken)
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
