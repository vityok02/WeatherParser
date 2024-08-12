using Application.Abstract;
using Application.Interfaces;
using Application.Messaging;
using Common.Constants;
using Domain.Abstract;

namespace Application.Features.Locations.LocationRequest;

internal sealed class LocationRequestCommandHandler
    : ICommandHandler<LocationRequestCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IUserStateRepository _userStateRepository;

    public LocationRequestCommandHandler(
        IMessageSender messageSender, IUserStateRepository userStateRepository)
    {
        _messageSender = messageSender;
        _userStateRepository = userStateRepository;
    }

    public async Task<Result> Handle(
        LocationRequestCommand command, CancellationToken cancellationToken)
    {
        _userStateRepository.SetState(command.ChatId, UserState.EnterLocation);

        await _messageSender.SendLocationRequestAsync(
            chatId: command.ChatId,
            messageText: "Enter your location.\n" +
            "Click the button or enter your place manually",
            buttonText: "Send geolocation",
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
