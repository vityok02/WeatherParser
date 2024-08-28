using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Common.Constants;
using Domain.Abstract;
using Domain.CachedLocations;

namespace Application.Commands.Requests.RequestPlaceName;

internal class RequestPlaceNameCommandHandler : ICommandHandler<RequestPlaceNameCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ISessionManager _sessionManager;
    private readonly IGeocodingService _geocodingService;
    private readonly IPlacesRepository _placesRepository;
    private readonly IKeyboardMarkupGenerator _keyboardMarkupGenerator;

    public RequestPlaceNameCommandHandler(
        IMessageSender messageSender,
        ISessionManager sessionManager,
        IGeocodingService geocodingService,
        IPlacesRepository cachedPlacesRepository,
        IKeyboardMarkupGenerator keyboardMarkupGenerator)
    {
        _messageSender = messageSender;
        _sessionManager = sessionManager;
        _geocodingService = geocodingService;
        _placesRepository = cachedPlacesRepository;
        _keyboardMarkupGenerator = keyboardMarkupGenerator;
    }

    public async Task<Result> Handle(RequestPlaceNameCommand command, CancellationToken cancellationToken)
    {
        if (command.PlaceName.Contains('/'))
        {
            var errorMessage = "Invalid input. Please provide a valid location name.";
            await _messageSender.SendTextMessageAsync(
                chatId: command.UserId,
                text: errorMessage,
                cancellationToken: cancellationToken);

            return Result.Success();
        }

        var result = await _geocodingService.GetPlacesByName(command.PlaceName, cancellationToken);
        if (result.IsFailure)
        {
            await _messageSender.SendTextMessageAsync(
                chatId: command.UserId,
                text: "Cannot get places",
                cancellationToken: cancellationToken);

            return Result.Failure(result.Error!);
        }

        var locations = result.Value;
        var locationsNames = locations!.Select(l => l.Name).ToArray();

        IAppReplyMarkup replyMarkup = _keyboardMarkupGenerator.BuildKeyboard(locationsNames!);

        _placesRepository.SetPlaces(command.UserId, locations!.Select(l => l.ToCachedLocation()).ToArray());

        var userSession = _sessionManager.GetOrCreateSession(command.UserId);
        userSession.Remove("state");
        userSession.Set("state", UserState.SetLocation);

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: "Select location from the list.\n" +
            "If you did not find the desired location, try entering your location again",
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
