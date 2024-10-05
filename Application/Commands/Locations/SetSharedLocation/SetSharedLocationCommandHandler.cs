using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Services;
using Domain.Abstract;
using Domain.Users;
using Domain.Locations;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Translations;

namespace Application.Locations.SetLocationFromRequest;

internal sealed class SetSharedLocationCommandHandler
    : ICommandHandler<SetSharedLocationCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ILogger<SetSharedLocationCommandHandler> _logger;
    private readonly IGeocodingService _geocodingService;
    private readonly IUserTranslationService _translation;
    private readonly IUserRepository _userRepository;
    private readonly IUserStateRepository _userStateRepository;

    public SetSharedLocationCommandHandler(
        IMessageSender messageSender,
        ILogger<SetSharedLocationCommandHandler> logger,
        IUserTranslationService translation,
        IGeocodingService geocodingService,
        IUserRepository userRepository,
        IUserStateRepository userStateRepository)
    {
        _messageSender = messageSender;
        _logger = logger;
        _translation = translation;
        _geocodingService = geocodingService;
        _userRepository = userRepository;
        _userStateRepository = userStateRepository;
    }

    public async Task<Result> Handle(
        SetSharedLocationCommand command,
        CancellationToken cancellationToken)
    {
        var language = await _translation.GetUserLanguage(
            command.UserId, cancellationToken);

        var translations = await _translation.GetUserTranslationAsync(
            command.UserId, cancellationToken);

        var result = await _geocodingService
            .GetPlaceName(command.Coordinates, language.Code, cancellationToken);
        
        if (result.IsFailure)
        {
            await _messageSender.SendTextMessageAsync(
                command.UserId,
                "Failed to retrieve location",
                cancellationToken);

            return Result.Failure(result.Error!);
        }

        var locationName = result.Value;

        var user = await _userRepository
            .GetByIdWithLocationsAsync(command.UserId, cancellationToken);

        if (!user!.Locations.Any(l => l.Name == locationName))
        {
            user!.SetCurrentLocation(new Location(locationName, command.Coordinates));
            await _userRepository.SaveChangesAsync(cancellationToken);
        }

        await _messageSender.SendTextMessageAsync(
            command.UserId,
            translations.Messages["LocationSet"],
            cancellationToken);

        _userStateRepository
            .RemoveState(command.UserId);

        return Result.Success();
    }
}
