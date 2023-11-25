using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using WeatherParser;
using System.Text;

var botClient = new TelegramBotClient("6305724672:AAHDSLwi05Y7_8LJB1TuoKzFGZhanne5ksI");

using CancellationTokenSource cts = new();

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
{
    if (update.Message is not { } message)
    {
        return;
    }

    if (message.Text is not { } messageText)
    {
        return;
    }

    var chatId = message.Chat.Id;


    var weather = new Weather();

    weather.GetWeather();

    var sb = new StringBuilder();
    sb.AppendLine($"Current temperature: {weather.CurrentTemperature.ToString()}");
    sb.AppendLine($"Minimal temperature: {weather.MinTemperature.ToString()}");
    sb.AppendLine($"Maximum temperature: {weather.MaxTemperature.ToString()}");
    sb.AppendLine($"Time: {weather.CurrentTime.ToString()}");
    sb.AppendLine($"Location: {weather.Location}");

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: sb.ToString(),
        cancellationToken: token);
}

Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
{
    var errorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(errorMessage);
    return Task.CompletedTask;
}