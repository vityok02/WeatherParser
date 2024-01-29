using WeatherParser.Handlers.CommandHandlers;
using WeatherParser.Models.Constants;
using WeatherParser.Models.Interfaces;
using WeatherParser.Services.BotServices;

namespace WeatherParser.Services;

public class BotCommandHandlerSelector : IBotCommandHandlerSelector
{
    private readonly IServiceProvider _serviceProvider;

    public BotCommandHandlerSelector(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IBotCommandHandler GetBotCommandHandler(string command)
    {
        return command switch
        {
            BotCommands.Weather => _serviceProvider.GetRequiredService<WeatherBotCommandHandler>(),
            BotCommands.Location => _serviceProvider.GetRequiredService<LocationBotCommandHandler>(),
            _ => _serviceProvider.GetRequiredService<DefaultBotCommandHandler>()
        };
    }
}
