using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Bot.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapBotWebhook(
        this WebApplication app,
        string path = "/bot")
    {
        app.MapPost(path, async (
            [FromBody] Update update,
            [FromServices] IUpdateHandler handler,
            [FromServices] ITelegramBotClient client,
            CancellationToken cancellationToken) =>
        {
            await handler.HandleUpdateAsync(
                client,
                update,
                cancellationToken);
        });

        return app;
    }
}
