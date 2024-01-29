using Telegram.Bot.Types;
using WeatherParser.Bot.Features.MessageSender;
using WeatherParser.Data.Repositories.CacheRepositories;
using WeatherParser.Features.Geocoding.GeocodingRecords;
using WeatherParser.Features.User;
using WeatherParser.Features.UserState.Constants;
using WeatherParser.Models;

namespace WeatherParser.Features.UserState.UserStateHandlers;

public class SetLocationStateHandler : IUserStateHandler
{
    private readonly IUserService _userService;
    private readonly IMessageSenderForLocationService _messageSender;
    private readonly IUserStateService _userStateService;
    private readonly LocationsCacheRepository _locationsCacheRepository;
    private readonly EnterLocationStateHandler _enterLocationStateHandler;

    public SetLocationStateHandler(IUserService userService, IMessageSenderForLocationService messageSender, IUserStateService userStateService, LocationsCacheRepository locationsCacheRepository, EnterLocationStateHandler enterLocationStateHandler)
    {
        _userService = userService;
        _messageSender = messageSender;
        _userStateService = userStateService;
        _locationsCacheRepository = locationsCacheRepository;
        _enterLocationStateHandler = enterLocationStateHandler;
    }

    public async Task<Message> HandleAsync(long userId, string text, CancellationToken cancellationToken = default)
    {
        var locations = _locationsCacheRepository.GetCache<Feature[]>(userId);

        if (locations is null)
        {
            return await HandleEnterLocationStateAsync(userId, text, cancellationToken);
        }

        var location = locations.FirstOrDefault(l => l.FullPlaceName == text);

        if (location is null)
        {
            return await HandleEnterLocationStateAsync(userId, text, cancellationToken);
        }

        var coordinates = new Coordinates(location.Center[1], location.Center[0]);

        var locationToSet = new Models.Location(location.FullPlaceName, coordinates);

        await _userService.SetUserLocationAsync(userId, locationToSet, cancellationToken);
        _userStateService.RemoveUserState(userId);

        return await _messageSender.SendSuccessfullyLocationSetMessage(userId, cancellationToken);
    }

    private async Task<Message> HandleEnterLocationStateAsync(long userId, string text, CancellationToken cancellationToken)
    {
        _userStateService.SetUserState(userId, UserStates.EnterLocation);
        return await _enterLocationStateHandler.HandleAsync(userId, text, cancellationToken);
    }
}
