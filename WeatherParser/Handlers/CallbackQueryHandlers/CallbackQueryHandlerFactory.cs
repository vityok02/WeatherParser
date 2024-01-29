using WeatherParser.Models.Constants;

namespace WeatherParser.Handlers.CallbackQueryHandlers;

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