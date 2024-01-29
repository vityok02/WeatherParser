using WeatherParser.Services.BotServices;

namespace WeatherParser.Models.Interfaces;

public interface IBotCommandHandlerSelector
{
    IBotCommandHandler GetBotCommandHandler(string command);
}
