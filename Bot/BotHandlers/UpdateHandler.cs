using Bot.Messages;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Bot.BotHandlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IMessageHandler _messageHandler;

    public UpdateHandler(
        ILogger<UpdateHandler> logger,
        IMessageHandler messageHandler)
    {
        _logger = logger;
        _messageHandler = messageHandler;
    }

    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message is Message message)
        {
            await _messageHandler
                .HandleMessage(message, cancellationToken);
        }

        _logger.LogDebug(
            "Received an unsupported update type: {UpdateType}.",
            update.Type);
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception ex, CancellationToken cancellationToken)
    {
        var errorMessage = ex switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => ex.ToString()
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", errorMessage);

        if (ex is RequestException)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }
}