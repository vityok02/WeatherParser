using Application;
using Bot;
using Bot.Configurations;
using Bot.Extensions;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Telegram.Bot;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.Configure<BotConfiguration>(
        context.Configuration.GetSection(BotConfiguration.Configuration));

    services.AddHttpClient("telegram_bot_client")
        .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
        {
            BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
            TelegramBotClientOptions options = new(botConfig.BotToken);
            return new TelegramBotClient(options, httpClient);
        });

    services.AddApplication();
    services.AddInfrastructure(context.Configuration);
    services.AddPresentation();
});

builder.UseSerilog((context, config) =>
        config.ReadFrom.Configuration(context.Configuration));

var host = builder.Build();

try
{
    using var dbContext = host.Services.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}
catch (Exception ex)
{
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Database was not created");
}

await host.RunAsync();