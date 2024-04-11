using Application.Features.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Application.BotHandlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public UpdateHandler(
        ILogger<UpdateHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Find the best solution for the situation
        var handler = update switch
        {
            { Message: { } message } => _serviceProvider.GetRequiredService<BotMessageHandler>().HandleMessage(message, cancellationToken),
            { CallbackQuery: { } callbackQuery } => _serviceProvider.GetRequiredService<CallbackQueryHandler>().HandleCallbackQuery(callbackQuery, cancellationToken),
            _ => _serviceProvider.GetRequiredService<DefaultHandler>().HandleDefault(update)
        };

        await handler;
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