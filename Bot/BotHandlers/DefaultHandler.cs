using Telegram.Bot.Types;

namespace Bot.BotHandlers;

public class DefaultHandler
{
    private readonly ILogger<DefaultHandler> _logger;

    public DefaultHandler(ILogger<DefaultHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleDefault(Update update)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);

        return Task.CompletedTask;
    }
}
