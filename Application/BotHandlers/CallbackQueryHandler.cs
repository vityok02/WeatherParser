using Application.Abstract;
using Application.Constants;
using Application.Features.CallbackQueryCommands;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Application.BotHandlers;

public class CallbackQueryHandler
{
    private readonly ILogger<CallbackQueryHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly ISender _sender;

    public CallbackQueryHandler(
        ILogger<CallbackQueryHandler> logger,
        IUserRepository userRepository,
        ISender sender,
        IUserStateRepository cachedUserStateRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _sender = sender;
    }

    public async Task HandleCallbackQuery(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        await _userRepository.EnsureCreate(callbackQuery.From.Id, callbackQuery.From.ToAppUser(), cancellationToken);


        if (callbackQuery.Data is null)
        {
            return;
        }

        ICommand command = callbackQuery.Data switch
        {
            CallbackData.SendPlaceName => new SendPlaceNameRequestCommand(callbackQuery.From.Id),
            CallbackData.SendGeolocationRequest => new SendGeolocationRequestCommand(callbackQuery.From.Id),
            _ => null!
        };

        if (command is not null)
        {
            await _sender.Send(command, cancellationToken);
        }

        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}
