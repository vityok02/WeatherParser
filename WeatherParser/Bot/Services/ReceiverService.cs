using Telegram.Bot;
using WeatherParser.Abstract;
using WeatherParser.Bot;

namespace WeatherParser.Bot.Bot.Services;

public class ReceiverService : ReceiverServiceBase<UpdateHandler>
{
    public ReceiverService(
        ITelegramBotClient botClient,
        UpdateHandler updateHandler,
        ILogger<ReceiverServiceBase<UpdateHandler>> logger)
        : base(botClient, updateHandler, logger)
    { }
}
