using Bot.BotHandlers;
using Bot.Abstract;
using Telegram.Bot;

namespace Bot.Services;

public class ReceiverService : ReceiverServiceBase<UpdateHandler>
{
    public ReceiverService(
        ITelegramBotClient botClient,
        UpdateHandler updateHandler,
        ILogger<ReceiverService> logger)
        : base(botClient, updateHandler, logger)
    { }
}
