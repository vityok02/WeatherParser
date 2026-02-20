using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Bot.Abstract;

public class ReceiverServiceBase<TUpdateHandler>
    : IReceiverService
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
            Limit = 100
        };

        var me = await _botClient
            .GetMe(cancellationToken);

        _logger.LogInformation(
            "Start receiving updates for {BotName}",
            me.Username ?? "My Awesome Bot");

        try
        {
            await _botClient.ReceiveAsync(
                updateHandler: _updateHandler,
                receiverOptions: receiverOptions,
                cancellationToken: cancellationToken);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Receiving was cancelled");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during ReceiveAsync");
            throw;
        }

        _logger.LogWarning("ReceiveAsync exited unexpectedly without exception");
    }
}
