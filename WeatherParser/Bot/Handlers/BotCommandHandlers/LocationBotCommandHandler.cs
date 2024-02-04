using Telegram.Bot.Types;
using WeatherParser.Features.Location;

namespace WeatherParser.Bot.Bot.Handlers.BotCommandHandlers;

public class LocationBotCommandHandler : IBotCommandHandler
{
    private readonly ISelectLocationSender _locationSelectionHandler;

    public LocationBotCommandHandler(ISelectLocationSender locationSelectionHandler)
    {
        _locationSelectionHandler = locationSelectionHandler;
    }

    public async Task<Message> HandleAsync(Message message, CancellationToken cancellationToken = default)
    {
        return await _locationSelectionHandler.SendSelectLocationMethodsAsync(message.From!.Id, cancellationToken);
    }
}
