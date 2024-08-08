using Application.Features.Weathers.SendForecastToday;
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

        services.AddScoped<TableConverter>();

        return services;
    }
}
