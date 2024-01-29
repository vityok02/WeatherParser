using WeatherParser.Abstract;
using WeatherParser.Bot;
using WeatherParser.Bot.Bot;
using WeatherParser.Bot.Bot.Handlers.BotCommandHandlers;
using WeatherParser.Bot.Bot.Handlers.BotHandlers;
using WeatherParser.Bot.Bot.Handlers.CallbackQueryHandlers;
using WeatherParser.Bot.Bot.Services;
using WeatherParser.Bot.Features.Location;
using WeatherParser.Bot.Features.MessageSender;
using WeatherParser.Bot.Features.Weather;
using WeatherParser.Data.Repositories.CacheRepositories;
using WeatherParser.Data.Repositories.UserRepository;
using WeatherParser.Features.GeocodingServices;
using WeatherParser.Features.Location;
using WeatherParser.Features.Timer;
using WeatherParser.Features.User;
using WeatherParser.Features.UserState;
using WeatherParser.Features.UserState.UserStateHandlers;
using WeatherParser.Features.Weather;
using WeatherParser.Models;

namespace WeatherParser;

public static class ServiceConfigurator
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddMemoryCache()
            .AddWeatherServices()
            .AddUserServices()
            .AddHandlers()
            .AddGeocodingServices()
            .AddScoped<ReceiverService>()
            .AddCacheRepositories()
            .AddScoped<ITimerService, TimerService>()
            .AddScoped<IConfigurationBuilder, ConfigurationBuilder>()
            .AddScoped<ILocationSelectionHandler, LocationSelectionHandler>()
            .AddScoped<IMessageSenderForLocationService, MessageSenderForLocationService>()
            .AddScoped<IUserStateService, UserStateService>()
            .AddScoped<ILocationRequester, LocationRequester>()
            ;
    }

    private static IServiceCollection AddCacheRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<LocationsCacheRepository>()
            .AddScoped<UserStateCacheRepository>()
            ;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        return services
            .AddBotCommandHandlers()
            .AddUserStateCommandHandlers()
            .AddCallbackQueryHandlers()
            .AddScoped<UpdateHandler>()
            .AddScoped<IBotMessageHandler, BotMessageHandler>()
            .AddScoped<IBotUpdateHandler, BotUpdateHandler>()
            ;
    }

    private static IServiceCollection AddBotCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<IBotCommandHandlerSelector, BotCommandHandlerSelector>()
            .AddScoped<LocationBotCommandHandler>()
            .AddScoped<WeatherBotCommandHandler>()
            .AddScoped<DefaultBotCommandHandler>()
            ;

    }

    private static IServiceCollection AddUserStateCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserStateHandlerFactory, UserStateHandlerFactory>()
            .AddScoped<EnterLocationStateHandler>()
            .AddScoped<SetLocationStateHandler>()
            ;
    }

    private static IServiceCollection AddCallbackQueryHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<ICallbackQueryHandlerFactory, CallbackQueryHandlerFactory>()
            .AddScoped<GeolocationQueryHandler>()
            .AddScoped<ByLocationNameQueryHandler>()
            ;
    }

    private static IServiceCollection AddWeatherServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IWeatherService, WeatherService>()
            .AddScoped<IWeatherSender, WeatherSender>()
            .AddScoped<IValidator<Weather>, WeatherValidator>()
            .AddScoped<IWeatherParserService, WeatherParserService>()
            .AddScoped<IWeatherUrlGenerator, WeatherUrlGenerator>()
            ;
    }

    private static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        return services
            .AddScoped<UserRepository>()
            .AddScoped<IUserRepository, CachedUserRepository>()
            .AddScoped<IUserService, UserService>()
            ;
    }

    private static IServiceCollection AddGeocodingServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IGeocodingUrlGenerator, GeocodingUrlGenerator>()
            .AddScoped<IGeocodingService, GeocodingService>()
            ;
    }
}
