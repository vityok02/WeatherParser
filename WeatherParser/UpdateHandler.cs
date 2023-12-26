using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherParser.Actions;
using WeatherParser.Data;
using WeatherParser.Extensions;
using WeatherParser.Models.Interfaces;

namespace WeatherParser.Handlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<UpdateHandler> _logger;
    private readonly AppDbContext _dbContext;
    private readonly IWeatherSender _weatherSender;
    private readonly ILocationRequester _locationRequester;

    public UpdateHandler(
        AppDbContext dbContext,
        IWeatherSender weatherSender,
        ITelegramBotClient botClient,
        ILogger<UpdateHandler> logger,
        ILocationRequester locationRequester)
    {
        _dbContext = dbContext;
        _weatherSender = weatherSender;
        _botClient = botClient;
        _logger = logger;
        _locationRequester = locationRequester;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not Message mes)
        {
            return;
        }

        if (mes.From is not User telegramUser)
        {
            return;
        }

        var user = telegramUser.ToAppUser();

        if (!await _dbContext.Users.AsNoTracking().AnyAsync(u => u.Id == user.Id, cancellationToken))
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        if (update.Message.Location is not null)
        {
            var location = update.Message.Location;

            user.SetLocation(location.Latitude, location.Longitude);
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _botClient.SendTextMessageAsync(
                chatId: mes.Chat.Id,
                text: "Thank you, your location has been set",
                cancellationToken: cancellationToken);
        }

        var handler = update switch
        {
            { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
            _ => UnknownUpdateHandlerAsync(update, cancellationToken)
        };

        await handler;
    }

    private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        if (message.Text is not { } messageText)
        {
            return;
        }

        var user = await _dbContext.Users.FindAsync(message.From!.Id);

        var action = messageText.Split(' ')[0] switch
        {
            // NOTE: consider using Strategy Pattern
            "/send_location" => _locationRequester.RequestLocation(message.Chat.Id, cancellationToken),
            "/weather" => _weatherSender.SendWeatherAsync(user!, cancellationToken),
            _ => Usage(_botClient, message, cancellationToken),
        };

        Message sentMessage = await action;

        _logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.MessageId);

        static async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            const string usage = "Usage:\n" +
                "/weather       - get weather information\n" +
                "/send_location - request location";

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }
    }

    private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception ex, CancellationToken cancellationToken)
    {
        var errorMessage = ex switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => ex.ToString()
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", errorMessage);

        if (ex is RequestException)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }
}
