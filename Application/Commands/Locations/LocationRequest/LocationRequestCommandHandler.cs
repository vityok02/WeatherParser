using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Common.Constants;
using Domain.Abstract;

namespace Application.Commands.Locations.LocationRequest;

internal sealed class LocationRequestCommandHandler
    : ICommandHandler<LocationRequestCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ISessionManager _sessionManager;

    public LocationRequestCommandHandler(
        IMessageSender messageSender, ISessionManager sessionManager)
    {
        _messageSender = messageSender;
        _sessionManager = sessionManager;
    }

    public async Task<Result> Handle(
        LocationRequestCommand command, CancellationToken cancellationToken)
    {
        var userSession = _sessionManager.GetOrCreateSession(command.ChatId);
        userSession.Set("state", UserState.EnterLocation);

        await _messageSender.SendLocationRequestAsync(
            chatId: command.ChatId,
            messageText: "Enter your location.\n" +
            "Click the button or enter your place manually",
            buttonText: "Send geolocation",
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
