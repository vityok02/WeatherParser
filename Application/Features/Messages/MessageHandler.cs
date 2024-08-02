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

public class MessageHandler
{
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly ISender _sender;
    private readonly IValidator<Message> _validator;
    private readonly ICachedUserStateRepository _cachedUserStateRepository;

    public MessageHandler(
        ILogger<UpdateHandler> logger,
        IUserRepository userRepository,
        ISender sender,
        IValidator<Message> validator,
        ICachedUserStateRepository cachedUserStateRepository)
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

        if (message?.Location is not null)
        {
            var setLocationFromRequestCommand = new SetLocationFromRequestCommand(userId, message.Location);
            await _sender.Send(setLocationFromRequestCommand, cancellationToken);
        }

        await _userRepository.EnsureCreate(userId, message!.From.ToAppUser(), cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        await ProcessBotCommand(message, userId, cancellationToken);
    }

    private async Task ProcessBotCommand(Message message, long userId, CancellationToken cancellationToken)
    {
        var userState = _cachedUserStateRepository.GetCache(userId);
        ICommand? userStateCommand = GetUserStateCommand(message, userId, userState);

        if (userStateCommand is not null)
        {
            await _sender.Send(userStateCommand, cancellationToken);
            return;
        }

        ICommand? botCommand = GetBotCommand(message, userId);

        if (botCommand is not null)
        {
            await _sender.Send(botCommand, cancellationToken);
            return;
        }

        await _sender.Send(new DefaultBotCommand(userId), cancellationToken);
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

    private static ICommand GetUserStateCommand(Message message, long userId, string? userState)
    {
        return userState switch
        {
            UserState.EnterLocation => new EnterPlaceNameCommand(userId, message!.Text!),
            UserState.SetLocation => new SetLocationCommand(userId, message!.Text!),
            _ => null!
        };
    }
}
