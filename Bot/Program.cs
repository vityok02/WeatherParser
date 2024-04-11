using Application;
using Bot;
using Bot.Polling;
using Bot.Reciever;
using Infrastructure;
using Infrastructure.Weathers.WeatherApi;
using Telegram.Bot;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<BotConfiguration>(
            context.Configuration.GetSection(BotConfiguration.Configuration));
        services.Configure<GeocodingConfiguration>(
            context.Configuration.GetSection(GeocodingConfiguration.Configuration));
        services.Configure<WeatherApiConfiguration>(
            context.Configuration.GetSection(WeatherApiConfiguration.Configuration));

        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
                TelegramBotClientOptions options = new(botConfig.BotToken);
                return new TelegramBotClient(options, httpClient);
            });

        services.AddApplication();
        services.AddInfrastructure(context.Configuration);

        services.AddScoped<ReceiverService>();

        services.AddHostedService<PollingService>();
    })
    .Build();


await host.RunAsync();