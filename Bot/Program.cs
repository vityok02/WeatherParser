using Application;
using Bot;
using Bot.Configurations;
using Bot.Extensions;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BotConfiguration>(
    builder.Configuration.GetSection(BotConfiguration.Configuration));

builder.Services.AddHttpClient("telegram_bot_client")
    .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
    {
        BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
        TelegramBotClientOptions options = new(botConfig.BotToken);
        return new TelegramBotClient(options, httpClient);
    });

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    using var scope = app.Services.CreateScope();
    using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    logger.LogInformation("Database was created");
    await DataSeeder.SeedDataAsync(dbContext);
    logger.LogInformation("Data was Seeded");
}
catch (Exception ex)
{
    logger.LogError(ex, "Database was not created");
}

app.MapGet("/", () => "Bot is running");

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://*:{port}");
