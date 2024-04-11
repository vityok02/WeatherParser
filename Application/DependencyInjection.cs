using Application.BotHandlers;
using Application.Features.Messages;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = AssemblyReference.Assembly;

        services.AddMediatR(configuration =>
            configuration
                .RegisterServicesFromAssembly(assembly));

        services
            .AddScoped<UpdateHandler>()
            .AddScoped<BotMessageHandler>()
            .AddScoped<CallbackQueryHandler>()
            .AddScoped<DefaultHandler>()
            .AddScoped<IValidator<Message>, BotMessageValidator>()
            ;

        return services;
    }
}
