using Telegram.Bot.Types;
using WeatherParser.Services.BotServices;
using WeatherParser.Services.WeatherServices;

namespace WeatherParser.Handlers.CommandHandlers;

public class WeatherBotCommandHandler : IBotCommandHandler
{
    private readonly IWeatherSender _weatherSender;

    public WeatherBotCommandHandler(IWeatherSender weatherSender)
    {
        _weatherSender = weatherSender;
    }

    public async Task<Message> HandleAsync(Message message, CancellationToken cancellationToken = default)
    {
        return await _weatherSender.SendWeatherAsync(message.From!.Id, cancellationToken);
    }
}
