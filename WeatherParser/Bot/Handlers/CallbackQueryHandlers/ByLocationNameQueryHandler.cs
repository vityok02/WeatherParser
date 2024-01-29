using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherParser.Features.UserState;
using WeatherParser.Features.UserState.Constants;

namespace WeatherParser.Bot.Bot.Handlers.CallbackQueryHandlers;

public class ByLocationNameQueryHandler : ICallbackQueryHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserStateService _userStateService;

    public ByLocationNameQueryHandler(ITelegramBotClient botClient, IUserStateService userStateService)
    {
        _botClient = botClient;
        _userStateService = userStateService;
    }

    public async Task<Message> HandleAsync(long userId, CancellationToken cancellationToken)
    {
        _userStateService.SetUserState(userId, UserStates.EnterLocation);

        return await _botClient.SendTextMessageAsync(
            chatId: userId,
            text: "Enter your town",
            cancellationToken: cancellationToken);
    }
}
