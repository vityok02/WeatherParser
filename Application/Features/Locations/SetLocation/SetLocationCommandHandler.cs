using Application.Abstract;
using Application.Features.Locations.EnterPlaceName;
using Application.Interfaces;
using Domain.Abstract;
using Domain.Locations;
using Domain.Users;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Features.Locations.SetLocation;

internal class SetLocationCommandHandler : ICommandHandler<SetLocationCommand>
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserRepository _userRepository;
    private readonly ICachedUserStateRepository _cachedUserStateRepository;
    private readonly ICachedPlacesRepository _cachedPlacesRepository;
    private readonly ISender _sender;

    public SetLocationCommandHandler(
        ITelegramBotClient botClient,
        IUserRepository userRepository,
        ICachedUserStateRepository cachedUserStateRepository,
        ICachedPlacesRepository cachedPlacesRepository,
        ISender sender)
    {
        _botClient = botClient;
        _userRepository = userRepository;
        _cachedUserStateRepository = cachedUserStateRepository;
        _cachedPlacesRepository = cachedPlacesRepository;
        _sender = sender;
    }

    public async Task<Result> Handle(SetLocationCommand command, CancellationToken cancellationToken)
    {
        var locations = _cachedPlacesRepository.GetCache(command.UserId);

        if (locations is null)
        {
            return await _sender
                .Send(new EnterPlaceNameCommand(command.UserId, command.PlaceName), cancellationToken);
        }

        var selectedLocation = locations.FirstOrDefault(l => l.PlaceName == command.PlaceName);

        if (selectedLocation is null)
        {
            return await _sender
                .Send(new EnterPlaceNameCommand(command.UserId, command.PlaceName), cancellationToken);
        }

        var locationToSet = selectedLocation.ToAppLocation();

        var user = await _userRepository.GetByIdWithLocationsAsync(command.UserId, cancellationToken);
        user!.SetCurrentLocation(locationToSet);

        _cachedUserStateRepository.RemoveCache(command.UserId);

        await _userRepository.SaveChangesAsync(cancellationToken);

        await _botClient.SendTextMessageAsync(
            chatId: command.UserId,
            text: "Location successfully set ✅",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
