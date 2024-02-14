using Domain.Users;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Application.BotHandlers;

public class CallbackQueryHandler
{
    private readonly ILogger<CallbackQueryHandler> _logger;
    private readonly IUserRepository _userRepository;

    public CallbackQueryHandler(ILogger<CallbackQueryHandler> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task HandleCallbackQuery(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        var ifUserExists = await _userRepository.CheckUserExistingAsync(callbackQuery.From.Id, cancellationToken);

        if (!ifUserExists)
        {
            await _userRepository.CreateAsync(callbackQuery.From.ToAppUser(), cancellationToken);
        }


        if (callbackQuery.Data is null)
        {
            return;
        }

        var handler = _callbackQueryHandlerFactory.GetHandler(callbackQuery.Data!);
        await handler.HandleAsync(callbackQuery.From.Id, cancellationToken)!;

        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}
