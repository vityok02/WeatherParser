using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Bot.Abstract;

public class ReceiverServiceBase<TUpdateHandler> : IReceiverService
    where TUpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly TUpdateHandler _updateHandler;
    private readonly ILogger<ReceiverServiceBase<TUpdateHandler>> _logger;

    internal ReceiverServiceBase(ITelegramBotClient client,
        TUpdateHandler updateHandler,
        ILogger<ReceiverServiceBase<TUpdateHandler>> logger)
    {
        _botClient = client;
        _updateHandler = updateHandler;
        _logger = logger;
    }

    public async Task ReceiveAsync(CancellationToken cancellationToken)
    {
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = [],
            ThrowPendingUpdates = true,
        };

        var me = await _botClient.GetMeAsync(cancellationToken);

        _logger.LogInformation("Start receiving updates for {BotName}", me.Username ?? "My Awesome Bot");

        await _botClient.ReceiveAsync(
            updateHandler: _updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken);
    }
}
