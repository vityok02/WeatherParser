using Bot.Configurations;
using Bot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Bot.Abstract;

public class BotWebhookService(
    ITelegramBotClient botClient,
    IOptionsMonitor<BotConfiguration> options,
    ILogger<BotWebhookService> logger)
    : IHostedService
{
    private readonly string _hostAddress = options
        .CurrentValue.HostAddress;

    public async Task StartAsync(
        CancellationToken cancellationToken)
    {
        var webhookUrl = $"{_hostAddress.TrimEnd('/')}/bot";

        logger.LogInformation(
            "Setting webhook to: {HostAddress}",
            _hostAddress);

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
