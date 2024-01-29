using Telegram.Bot.Types;
using WeatherParser.Models.Interfaces;
using WeatherParser.Services.BotServices;

namespace WeatherParser.Handlers.CommandHandlers;

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
