using Application.Abstract;
using Application.BotHandlers;
using Application.Constants;
using Application.Features.DefaultMessage;
using Application.Features.Locations;
using Application.Features.Locations.EnterPlaceName;
using Application.Features.Locations.SetLocation;
using Application.Features.Weathers;
using Application.Interfaces;
using Application.Locations.SetLocationFromRequest;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using BotCommand = Application.Constants.BotCommand;

namespace Application.Features.Messages;

public class BotMessageHandler
{
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly ISender _sender;
    private readonly IValidator<Message> _validator;
    private readonly IUserStateRepository _cachedUserStateRepository;

    public BotMessageHandler(
        ILogger<UpdateHandler> logger,
        IUserRepository userRepository,
        ISender sender,
        IValidator<Message> validator,
        IUserStateRepository cachedUserStateRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _sender = sender;
        _validator = validator;
        _cachedUserStateRepository = cachedUserStateRepository;
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

        await _userRepository.EnsureCreate(userId, message.From.ToAppUser(), cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        await ProcessCommand(message, userId, cancellationToken);
    }

    private async Task ProcessCommand(Message message,
        long userId,
        CancellationToken cancellationToken)
    {
        ICommand? command;

        if (message.Location is not null)
        {
            command = new SetLocationFromRequestCommand(userId, message.Location);
        }
        else
        {
            command = GetUserStateCommand(message, userId);
            if (command is null)
            {
                command = GetBotCommand(message, userId);
            }

            if (command is null)
            {
                command = new DefaultBotCommand(userId);
            }
        }

        await _sender.Send(command, cancellationToken);
    }

    private ICommand GetUserStateCommand(Message message, long userId)
    {
        var userState = _cachedUserStateRepository.GetUserState(userId);

        ICommand? userStateCommand = userState switch
        {
            UserState.EnterLocation => new EnterPlaceNameCommand(userId, message!.Text!),
            UserState.SetLocation => new SetLocationCommand(userId, message!.Text!),
            _ => null!
        };
        return userStateCommand;
    }

    private static ICommand GetBotCommand(Message message, long userId)
    {
        return message.Text!.Split(' ')[0] switch
        {
            BotCommand.Weather => new SendWeatherCommand(userId),
            BotCommand.Location => new LocationCommand(userId),
            _ => null!
        };
    }
}
