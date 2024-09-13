using Application.Commands.Default;
using Application.Commands.Requests.RequestPlaceName;
using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Application.Common.Interfaces.Repositories;
using Domain.Abstract;
using Domain.Locations;
using Domain.Users;
using MediatR;

namespace Application.Commands.Locations.SetLocation;
// TODO: many dependencies
internal sealed class SetLocationCommandHandler
    : ICommandHandler<SetLocationCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IUserRepository _userRepository;
    private readonly ISessionManager _sessionManager;
    private readonly IPlacesRepository _placesRepository;
    private readonly ISender _sender;
    private readonly IUserTranslationService _translationService;
    private readonly IDefaultKeyboardFactory _keyboardFactory;

    public SetLocationCommandHandler(
        IMessageSender messageSender,
        IUserRepository userRepository,
        ISessionManager sessionManager,
        IPlacesRepository cachedPlacesRepository,
        ISender sender,
        IUserTranslationService translationService,
        IDefaultKeyboardFactory keyboardFactory)
    {
        _messageSender = messageSender;
        _userRepository = userRepository;
        _sessionManager = sessionManager;
        _placesRepository = cachedPlacesRepository;
        _sender = sender;
        _translationService = translationService;
        _keyboardFactory = keyboardFactory;
    }

    public async Task<Result> Handle(
        SetLocationCommand command,
        CancellationToken cancellationToken)
    {
        var locations = _placesRepository
            .GetPlaces(command.UserId);

        // TODO: refuse to use the sender

        if (locations is null)
        {
            return await _sender
                .Send(new RequestPlaceNameCommand(
                    command.UserId,
                    command.PlaceName), cancellationToken);
        }

        var selectedLocation = locations
            .FirstOrDefault(l => l.PlaceName == command.PlaceName);

        if (selectedLocation is null)
        {
            return await _sender
                .Send(new RequestPlaceNameCommand(
                    command.UserId,
                    command.PlaceName), cancellationToken);
        }

        var locationToSet = selectedLocation.ToAppLocation();

        var user = await _userRepository
            .GetByIdWithLocationsAsync(command.UserId, cancellationToken);
        user!
            .SetCurrentLocation(locationToSet);

        var userSession = _sessionManager
            .GetOrCreateSession(command.UserId);
        userSession.Remove("state");

        await _userRepository.SaveChangesAsync(cancellationToken);

        var translation = await _translationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: translation.Messages["LocationSet"],
            replyMarkup: _keyboardFactory
            .CreateKeyboard(translation),
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
