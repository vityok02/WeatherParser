using Application;
using Bot;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

builder.Services.AddHealthChecks();

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    await ApplyMigrationAndSeedData(app);
}

app.MapPost("/bot", async (
    [FromBody] Update update,
    [FromServices] IUpdateHandler handler,
    [FromServices] ITelegramBotClient client,
    CancellationToken cancellationToken) =>
{
    await client.SendMessage(
        update.Message.From.Id,
        "I am Weather Bot");

    await handler.HandleUpdateAsync(
        client,
        update,
        cancellationToken);
});

app.UseHealthChecks("/health");

await app.RunAsync();

static async Task ApplyMigrationAndSeedData(WebApplication app)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    try
    {
        await using var scope = app.Services.CreateAsyncScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.MigrateAsync();
        await DataSeeder.SeedDataAsync(dbContext);

        logger.LogInformation("Database migration & seed completed");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database migration or seed failed");
        throw;
    }
}
