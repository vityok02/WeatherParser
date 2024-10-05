using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Translations;
using Common.Constants;
using Domain.Abstract;
using Domain.CachedLocations;

namespace Application.Commands.Requests.RequestPlaceName;

internal class RequestPlaceNameCommandHandler
    : ICommandHandler<RequestPlaceNameCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ISessionManager _sessionManager;
    private readonly IGeocodingService _geocodingService;
    private readonly IPlacesRepository _placesRepository;
    private readonly IKeyboardMarkupGenerator _keyboardMarkupGenerator;
    private readonly IUserTranslationService _translationService;

    public RequestPlaceNameCommandHandler(
        IMessageSender messageSender,
        ISessionManager sessionManager,
        IGeocodingService geocodingService,
        IPlacesRepository cachedPlacesRepository,
        IKeyboardMarkupGenerator keyboardMarkupGenerator,
        IUserTranslationService userTranslationService)
    {
        _messageSender = messageSender;
        _sessionManager = sessionManager;
        _geocodingService = geocodingService;
        _placesRepository = cachedPlacesRepository;
        _keyboardMarkupGenerator = keyboardMarkupGenerator;
        _translationService = userTranslationService;
    }

    public async Task<Result> Handle(
        RequestPlaceNameCommand command,
        CancellationToken cancellationToken)
    {
        var translation = await _translationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        if (command.PlaceName.Contains('/'))
        {
            await _messageSender.SendTextMessageAsync(
                chatId: command.UserId,
                text: translation.Messages["InvalidPlaceName"],
                cancellationToken: cancellationToken);

            return Result.Success();
        }

        var result = await _geocodingService
            .GetPlacesByName(command.PlaceName, cancellationToken);
        if (result.IsFailure)
        {
            await _messageSender.SendTextMessageAsync(
                chatId: command.UserId,
                text: translation.Messages["RequestPlaceFail"],
                cancellationToken: cancellationToken);

            return Result.Failure(result.Error!);
        }

        var locations = result.Value;
        var locationsNames = locations!
            .Select(l => l.Name)
            .ToArray();

        IAppReplyMarkup replyMarkup = _keyboardMarkupGenerator
            .BuildKeyboard(locationsNames!);

        _placesRepository
            .SetPlaces(command.UserId, locations!
                .Select(l => l.ToCachedLocation())
                .ToArray());

        var userSession = _sessionManager
            .GetOrCreateSession(command.UserId);
        userSession
            .Remove("state");
        userSession
            .Set("state", UserState.SetLocation);

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: translation.Messages["RequestPlaceName"],
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
