using WeatherParser.Bot.Bot.Handlers.BotCommandHandlers;
using WeatherParser.Bot.Constants;

namespace WeatherParser.Bot;

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
