using Domain.Users;
using Telegram.Bot.Types;

namespace Bot.BotHandlers;

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

        var userId = callbackQuery.From.Id;
        await _userRepository.EnsureCreateAsync(userId, cancellationToken);


        if (callbackQuery.Data is null)
        {
            return;
        }

        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}
