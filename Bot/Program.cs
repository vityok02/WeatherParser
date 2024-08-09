using Application;
using Bot;
using Bot.Extensions;
using Infrastructure;
using Infrastructure.Data;
using Telegram.Bot;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
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
    })
    .Build();

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