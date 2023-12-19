using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using WeatherParser;
using WeatherParser.Data;
using WeatherParser.Extensions;
using WeatherParser.Services;

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

        services.AddServices();

        services.AddDbContext<AppDbContext>((options) => 
            options.UseSqlServer(context.Configuration
                .GetConnectionString("LocalSqlServer")));

        services.AddHostedService<PollingService>();
    })
    .Build();

await host.RunAsync();
