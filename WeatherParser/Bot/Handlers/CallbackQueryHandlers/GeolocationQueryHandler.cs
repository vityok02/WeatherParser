using Telegram.Bot.Types;
using WeatherParser.Features.Location;

namespace WeatherParser.Bot.Bot.Handlers.CallbackQueryHandlers;

public class GeolocationQueryHandler : ICallbackQueryHandler
{
    private readonly ILocationRequester _locationRequester;

    public GeolocationQueryHandler(ILocationRequester locationRequester)
    {
        _locationRequester = locationRequester;
    }

    public async Task<Message> HandleAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _locationRequester.RequestLocationAsync(userId, cancellationToken);
    }
}