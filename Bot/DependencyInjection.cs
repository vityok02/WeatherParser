using Application.Interfaces;
using Bot.BotHandlers;
using Bot.Messages;
using Bot.Services;
using Bot.TgTypes;
using Telegram.Bot.Types;

namespace Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<ReceiverService>()
            .AddHostedService<PollingService>()
            .AddScoped<UpdateHandler>()
            .AddScoped<MessageHandler>()
            .AddScoped<CallbackQueryHandler>()
            .AddScoped<DefaultHandler>()
            .AddScoped<IValidator<Message>, MessageValidator>()
            .AddScoped<IMessageSender, TgMessageSender>()
            .AddScoped<GetLocationRequest>()
            .AddScoped<DefaultMessageHandler>()
            .AddScoped<IApplicationReplyMarkup, TgReplyMarkup>()
            .AddScoped<IReplyKeyboardRemove, TgRemoveKeyboardMarkup>()
            .AddScoped<IAppFile, TgFile>()
            ;

        return services;
    }
}
