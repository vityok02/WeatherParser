using Telegram.Bot.Types;
using WeatherParser.Features.Weather;

namespace WeatherParser.Bot.Bot.Handlers.BotCommandHandlers;

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
