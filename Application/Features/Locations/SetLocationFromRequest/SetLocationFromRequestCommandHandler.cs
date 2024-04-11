using Application.Abstract;
using Application.Interfaces;
using Domain.Locations;
using Domain.Users;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Locations.SetLocationFromRequest;

internal class SetLocationFromRequestCommandHandler : ICommandHandler<SetLocationFromRequestCommand>
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserRepository _userRepository;
    private readonly IGeocodingService _geocodingService;

    public SetLocationFromRequestCommandHandler(
        IUserRepository userRepository,
        IGeocodingService geocodingService,
        ITelegramBotClient botClient)
    {
        _userRepository = userRepository;
        _geocodingService = geocodingService;
        _botClient = botClient;
    }

    //get location by coordinates
    //set user location
    //return successfull message

    public async Task<Message> Handle(
        SetLocationFromRequestCommand command,
        CancellationToken cancellationToken)
    {
        Coordinates coordinates = new Coordinates(
            command.Location.Latitude,
            command.Location.Longitude);

        Domain.Locations.Location location = new(null!, coordinates);

        Domain.Users.User? user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        user!.SetCurrentLocation(location);

        await _userRepository.SaveChangesAsync(cancellationToken);
        await Console.Out.WriteLineAsync(coordinates.ToString());

        return await _botClient.SendTextMessageAsync(
            chatId: user.Id,
            text: "Location successfully set✅",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }
}

