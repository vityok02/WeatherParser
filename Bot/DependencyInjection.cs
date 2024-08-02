using Bot.Services;

namespace Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();

        return services;
    }
}
