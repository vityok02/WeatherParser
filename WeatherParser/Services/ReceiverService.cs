using Telegram.Bot;
using WeatherParser.Abstract;
using WeatherParser.Handlers;

namespace WeatherParser.Services;

public class ReceiverService : ReceiverServiceBase<UpdateHandler>
{
    public ReceiverService(
        ITelegramBotClient botClient,
        UpdateHandler updateHandler,
        ILogger<ReceiverServiceBase<UpdateHandler>> logger)
        : base(botClient, updateHandler, logger)
    { }
}
