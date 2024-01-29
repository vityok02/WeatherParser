using WeatherParser.Bot.Bot.Handlers.CallbackQueryHandlers;
using WeatherParser.Bot.Constants;

namespace WeatherParser.Bot.Bot;

public class CallbackQueryHandlerFactory : ICallbackQueryHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CallbackQueryHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICallbackQueryHandler GetHandler(string query)
    {
        return query switch
        {
            CallbackDatas.Geolocation => _serviceProvider.GetRequiredService<GeolocationQueryHandler>(),
            CallbackDatas.ByLocationName => _serviceProvider.GetRequiredService<ByLocationNameQueryHandler>(),
            _ => default!
        };
    }
}