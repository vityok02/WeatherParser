using Bot.Configurations;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Bot.Abstract;

public class BotWebhookService(
    ITelegramBotClient botClient,
    IOptionsMonitor<BotConfiguration> options,
    ILogger<BotWebhookService> logger)
    : IHostedService
{
    public async Task StartAsync(
        CancellationToken cancellationToken)
    {
        var hostAddress = options.CurrentValue.HostAddress;
        var webhookUrl = $"{hostAddress.TrimEnd('/')}/bot";

        logger.LogInformation(
            "Setting webhook to: {WebhookUrl}",
            webhookUrl);

        await botClient.SetWebhook(
            webhookUrl,
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(
        CancellationToken cancellationToken)
    {
        await botClient.DeleteWebhook(
            cancellationToken: cancellationToken);
    }
}
