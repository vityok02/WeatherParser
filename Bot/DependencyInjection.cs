using Application.Commands.Default;
using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Application.Keyboard;
using Bot.BotHandlers;
using Bot.Configurations;
using Bot.Extensions;
using Bot.Messages;
using Bot.Services;
using Bot.TgTypes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<BotConfiguration>(
            configuration.GetSection(BotConfiguration.Configuration));

        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
                TelegramBotClientOptions options = new(botConfig.BotToken);
                return new TelegramBotClient(options, httpClient);
            });

        services
            .AddScoped<ReceiverService>()
            .AddHostedService<PollingService>()
            .AddScoped<UpdateHandler>()
            .AddScoped<IMessageHandler, MessageHandler>()
            .AddScoped<DefaultHandler>()
            .AddScoped<IValidator<Message>, MessageValidator>()
            .AddScoped<IMessageSender, TelegramMessageSender>()
            .AddScoped<IKeyboardMarkupGenerator, KeyboardMarkupGenerator>()
            .AddScoped<IRemoveKeyboardMarkup, RemoveKeyboardMarkup>()
            .AddScoped<IDefaultKeyboardFactory, DefaultKeyboardFactory>()

            ;

        return services;
    }
}
