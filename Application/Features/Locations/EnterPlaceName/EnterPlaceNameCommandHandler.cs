using Application.Abstract;
using Application.Constants;
using Application.Interfaces;
using Domain.CachedLocations;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Features.Locations.EnterPlaceName;

internal class EnterPlaceNameCommandHandler : ICommandHandler<EnterPlaceNameCommand>
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserStateRepository _userStateRepository;
    private readonly IGeocodingService _geocodingService;
    private readonly IPlacesRepository _placesRepository;

    public EnterPlaceNameCommandHandler(
        ITelegramBotClient botClient,
        IUserStateRepository cachedUserStateRepository,
        IGeocodingService geocodingService,
        IPlacesRepository cachedPlacesRepository)
    {
        _botClient = botClient;
        _userStateRepository = cachedUserStateRepository;
        _geocodingService = geocodingService;
        _placesRepository = cachedPlacesRepository;
    }

    public async Task<Message> Handle(EnterPlaceNameCommand command, CancellationToken cancellationToken)
    {
        if (command.PlaceName.Contains('/'))
        {
            var errorMessage = "Invalid input. Please provide a valid location name.";
            return await _botClient.SendTextMessageAsync(
                chatId: command.UserId,
                text: errorMessage,
                cancellationToken: cancellationToken);
        }

        var result = await _geocodingService.GetLocations(command.PlaceName, cancellationToken);
        if (result.IsFailure)
        {
            return await _botClient.SendTextMessageAsync(
                chatId: command.UserId,
                text: result.Error.Description,
                cancellationToken: cancellationToken);
        }

        var locations = result.Value;
        var locationsNames = locations!.Select(l => l.FullPlaceName).ToArray();

        ReplyKeyboardMarkup replyMarkup = ReplyMarkupHelper.GetReplyKeyboardMarkup(locationsNames!);

        _placesRepository.SetPlaces(command.UserId, locations!.Select(l => l.ToCachedLocaion()).ToArray());
        _userStateRepository.UpdateUserState(command.UserId, UserState.SetLocation);

        return await _botClient.SendTextMessageAsync(
            chatId: command.UserId,
            text: "Select location from the list.\n" +
            "If you did not find the desired location, try entering your location again",
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);
    }
}
