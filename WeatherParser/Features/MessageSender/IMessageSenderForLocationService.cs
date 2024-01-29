using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherParser.Bot.Features.MessageSender;

public interface IMessageSenderForLocationService : IMessageSender
{
    Task<Message> SendNotFoundLocationsMessage(long chatId, CancellationToken cancellationToken = default);
    Task<Message> SendSelectLocationMessage(long chatId, ReplyKeyboardMarkup replyMarkup, CancellationToken cancellationToken = default);
    Task<Message> SendSuccessfullyLocationSetMessage(long chatId, CancellationToken cancellationToken = default);
}