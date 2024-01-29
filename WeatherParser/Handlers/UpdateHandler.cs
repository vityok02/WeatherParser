using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using WeatherParser.Extensions;
using WeatherParser.Models.Interfaces;
using WeatherParser.Services.BotServices;

namespace WeatherParser.Handlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IBotUpdateHandler _botUpdateHandler;
    private readonly IUserService _userService;

    public UpdateHandler(
        ILogger<UpdateHandler> logger,
        IUserService userService,
        IBotUpdateHandler botUpdateHandler)
    {
        _logger = logger;
        _userService = userService;
        _botUpdateHandler = botUpdateHandler;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await AddUserIfNotExists(update, cancellationToken);

        await _botUpdateHandler.HandleAsync(update, cancellationToken);
    }

    private async Task AddUserIfNotExists(Update update, CancellationToken cancellationToken)
    {
        if (update.Message is Message message &&
            message.From is User telegramUser)
        {
            bool isUserInDb = await _userService.IsUserInDbAsync(telegramUser.Id, cancellationToken);

            if (!isUserInDb)
            {
                await _userService.AddUserToDbAsync(telegramUser.ToAppUser(), cancellationToken);
            }
        }
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
