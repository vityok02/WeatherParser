using Telegram.Bot.Types;
using WeatherParser.Features.Location;

namespace WeatherParser.Bot.Bot.Handlers.BotCommandHandlers;

public class LocationBotCommandHandler : IBotCommandHandler
{
    private readonly ILocationSelectionHandler _locationSelectionHandler;

    public LocationBotCommandHandler(ILocationSelectionHandler locationSelectionHandler)
    {
        _locationSelectionHandler = locationSelectionHandler;
    }

    public async Task<Message> HandleAsync(Message message, CancellationToken cancellationToken = default)
    {
        return await _locationSelectionHandler.SendLocationSelectionMessageAsync(message, cancellationToken);
    }
}
