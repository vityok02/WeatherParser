using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherParser.Bot.Features.MessageSender;

public class MessageSender : IMessageSender
{
    private readonly ITelegramBotClient _botClient;

    public MessageSender(ITelegramBotClient telegramBotClient)
    {
        _botClient = telegramBotClient;
    }

    public async Task<Message> SendMessageAsync(
    long chatId,
    string text,
    CancellationToken cancellationToken = default)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            cancellationToken: cancellationToken);
    }
}

public class MessageSenderForLocationService : MessageSender, IMessageSenderForLocationService
{
    private readonly ITelegramBotClient _botClient;

    public MessageSenderForLocationService(ITelegramBotClient botClient)
        : base(botClient)
    {
        _botClient = botClient;
    }

    public async Task<Message> SendNotFoundLocationsMessage(
        long chatId,
        CancellationToken cancellationToken = default)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Sorry, the geolocation for the entered text could not be found.\n" +
                "Try another query or enter a more specific query.",
            cancellationToken: cancellationToken);
    }

    public async Task<Message> SendSelectLocationMessage(
        long chatId,
        ReplyKeyboardMarkup replyMarkup,
        CancellationToken cancellationToken = default)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Select location from the list.\n" +
            "If you did not find the desired location, try entering your location again",
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);
    }

    public async Task<Message> SendSuccessfullyLocationSetMessage(
        long chatId,
        CancellationToken cancellationToken = default)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Location successfully set ✅",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }
}