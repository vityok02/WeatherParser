using Application.BotHandlers;
using Application.Features.Messages;
using Microsoft.Extensions.DependencyInjection;

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
            .AddScoped<MessageHandler>()
            .AddScoped<CallbackQueryHandler>()
            .AddScoped<DefaultHandler>()
            ;

        return services;
    }
}
