using Application.Abstract;
using Application.Constants;
using Application.Features.Locations.SetLocation;
using Application.Features.Weathers.SendForecastToday;
using Application.Features.Weathers.SendWeatherNow;
using Application.Interfaces;
using Bot.BotHandlers;
using Bot.Services;
using Domain.Locations;
using Domain.Users;
using MediatR;
using Telegram.Bot.Types;
using BotCommand = Bot.Constants.BotCommand;

namespace Bot.Messages;

public class MessageHandler
{
    private readonly ILogger<MessageHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly ISender _sender;
    private readonly IValidator<Message> _validator;
    private readonly IUserStateRepository _userStateRepository;
    private readonly IServiceProvider _sp;
    private readonly LocationRequestHandler _locationRequestHandler;

    public MessageHandler(
        ILogger<MessageHandler> logger,
        IUserRepository userRepository,
        ISender sender,
        IValidator<Message> validator,
        IUserStateRepository userStateRepository,
        IServiceProvider sp,
        LocationRequestHandler locationRequestHandler)
    {
        _logger = logger;
        _userRepository = userRepository;
        _sender = sender;
        _validator = validator;
        _userStateRepository = userStateRepository;
        _sp = sp;
        _locationRequestHandler = locationRequestHandler;
    }

    public async Task HandleMessage(Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        var validationResult = _validator.Validate(message);
        if (validationResult.IsError)
        {
            _logger.LogError("Invalid message: {validationResult}", validationResult.ToString());
        }

        var userId = message.From!.Id;

        await _userRepository.EnsureCreate(userId, new Domain.Users.User(userId), cancellationToken);

        if (!await _userRepository.HasLocationAsync(userId, cancellationToken))
        {
            await _locationRequestHandler.RequestLocation(userId, cancellationToken);

            return;
        }

        await _userRepository.SaveChangesAsync(cancellationToken);

        await ProcessBotCommand(message, userId, cancellationToken);
    }

    private async Task ProcessBotCommand(Message message, long userId, CancellationToken cancellationToken)
    {
        var userState = _userStateRepository.GetState(userId);

        if (userState == UserState.SetLocation)
        {
            Coordinates? coordinates = null;
            
            if (message.Location is Telegram.Bot.Types.Location requestLocation)
            {
                coordinates = new(requestLocation.Latitude, requestLocation.Longitude);
            }

            var setLocationCommand = new SetLocationCommand(userId, coordinates, message.Text!);
            await _sender.Send(setLocationCommand, cancellationToken);

            return;
        }

        var user = await _userRepository.GetByIdWithLocationsAsync(userId, cancellationToken);
        ICommand? botCommand = GetBotCommand(message, userId, user!.CurrentLocation!.Coordinates!);

        if (message.Text == BotCommand.Location)
        {
            await _locationRequestHandler.RequestLocation(userId, cancellationToken);
            return;
        }

        if (botCommand is not null)
        {
            await _sender.Send(botCommand, cancellationToken);
            return;
        }


        var defaultMessageHandler = _sp.GetRequiredService<DefaultMessageHandler>();
        await defaultMessageHandler.Handle(userId, cancellationToken);
    }

    private static ICommand GetBotCommand(Message message, long userId, Coordinates coordinates)
    {
        var botCommand = message.Text!.Split(' ').FirstOrDefault();

        return botCommand switch
        {
            BotCommand.WeatherNow => new SendWeatherNowCommand(userId, coordinates),
            BotCommand.ForecastToday => new SendForecastTodayCommand(userId, coordinates),
            _ => null!
        };
    }
}
