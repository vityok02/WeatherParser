using WeatherParser.Handlers;
using WeatherParser.Models.Interfaces;
using WeatherParser.Services;

namespace WeatherParser;

public static class ServiceConfigurator
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<UpdateHandler>()
            .AddScoped<ReceiverService>()
            .AddScoped<IWeatherService, WeatherService>()
            .AddScoped<IWeatherSender, WeatherSender>()
            .AddScoped<IWeatherValidator, WeatherValidator>()
            .AddScoped<IWeatherParserService, WeatherParserService>()
            .AddScoped<IHtmlPathBuilder, HtmlPathBuilder>()
            .AddScoped<ITimerService, TimerService>()
            .AddScoped<IConfigurationBuilder, ConfigurationBuilder>();

        return services;
    }
}
