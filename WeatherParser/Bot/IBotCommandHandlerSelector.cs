using WeatherParser.Bot.Bot.Handlers.BotCommandHandlers;

namespace WeatherParser.Bot;

public interface IBotCommandHandlerSelector
{
    IBotCommandHandler GetBotCommandHandler(string command);
}
