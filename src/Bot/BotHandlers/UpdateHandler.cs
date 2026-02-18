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
    }

    public async Task HandleErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram API Error: [{apiRequestException.ErrorCode}]. {apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogError(
            exception,
            "HandleError from {Source}: {ErrorMessage}",
            source,
            errorMessage);

        if (exception is RequestException)
        {
            _logger.LogWarning(
                "Request exception detected, waiting before retry");

            await Task.Delay(
                TimeSpan.FromSeconds(5),
                cancellationToken);
        }
    }
}