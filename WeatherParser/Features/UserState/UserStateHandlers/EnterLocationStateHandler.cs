using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherParser.Bot.Features.MessageSender;
using WeatherParser.Data.Repositories.CacheRepositories;
using WeatherParser.Features.GeocodingServices;
using WeatherParser.Features.UserState.Constants;

namespace WeatherParser.Features.UserState.UserStateHandlers;

public class EnterLocationStateHandler : IUserStateHandler
{
    private readonly LocationsCacheRepository _locationsCacheRepository;
    private readonly IGeocodingService _geocodingService;
    private readonly IMessageSenderForLocationService _messageSender;
    private readonly IUserStateService _userStateService;

    public EnterLocationStateHandler(
        IGeocodingService geocodingService,
        IMessageSenderForLocationService messageSender,
        LocationsCacheRepository locationsCacheMemory,
        IUserStateService userStateService)
    {
        _geocodingService = geocodingService;
        _messageSender = messageSender;
        _locationsCacheRepository = locationsCacheMemory;
        _userStateService = userStateService;
    }

    public async Task<Message> HandleAsync(long userId, string text, CancellationToken cancellationToken = default)
    {
        if (text.Contains('/'))
        {
            var errorMessage = "Invalid input. Please provide a valid location name.";
            return await _messageSender.SendMessageAsync(userId, errorMessage, cancellationToken);
        }

        var result = await _geocodingService.GetLocationsByName(text, cancellationToken);
        if (result.IsFailure)
        {
            return await _messageSender.SendMessageAsync(userId, result.Error!.Description, cancellationToken);
        }

        var locations = result.Value;
        var locationsNames = locations!.Select(l => l.FullPlaceName).ToArray();

        ReplyKeyboardMarkup replyMarkup = ReplyMarkupHelper.GetReplyKeyboardMarkup(locationsNames!);

        _locationsCacheRepository.SetCache(userId, locations);
        _userStateService.ChangeUserState(userId, UserStates.SetLocation);

        return await _messageSender.SendSelectLocationMessage(userId, replyMarkup, cancellationToken);
    }
}