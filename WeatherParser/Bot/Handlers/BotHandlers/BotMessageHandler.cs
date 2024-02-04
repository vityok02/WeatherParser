using Telegram.Bot.Types;
using WeatherParser.Bot.Bot.Handlers.CallbackQueryHandlers;
using WeatherParser.Features.UserState;
using WeatherParser.Features.UserState.UserStateHandlers;

namespace WeatherParser.Bot.Bot.Handlers.BotHandlers;

public class BotMessageHandler : IBotMessageHandler
{
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IUserStateHandlerFactory _userStateHandlerFacotry;
    private readonly IBotCommandHandlerSelector _handlerSelector;
    private readonly IUserStateService _userStateService;
    private readonly ICallbackQueryHandlerFactory _callbackQueryHandlerFactory;

    public BotMessageHandler(
        ILogger<UpdateHandler> logger,
        IBotCommandHandlerSelector actionSelector,
        IUserStateService userStateService,
        IUserStateHandlerFactory userStateHandlerFacotry,
        ICallbackQueryHandlerFactory callbackQueryHandlerFactory)
    {
        _logger = logger;
        _handlerSelector = actionSelector;
        _userStateService = userStateService;
        _userStateHandlerFacotry = userStateHandlerFacotry;
        _callbackQueryHandlerFactory = callbackQueryHandlerFactory;
    }

    public async Task HandleMessage(Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        if (message!.Text is not { } messageText)
        {
            return;
        }

        if (message?.Location is not null)
        {
            // TODO: set user location
        }

        var userState = _userStateService.GetUserState(message!.From!.Id);
        if (userState is not null)
        {
            var stateHandler = _userStateHandlerFacotry.GetUserStateHandler(userState!);
            await stateHandler.HandleAsync(message.From.Id, messageText, cancellationToken);

            return;
        }

        var botCommand = messageText.Split(' ')[0];
        var botCommandHandler = _handlerSelector.GetBotCommandHandler(botCommand);

        var sentMessage = await botCommandHandler.HandleAsync(message, cancellationToken);

        _logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.MessageId);
    }

    public async Task HandleCallbackQuery(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        if (callbackQuery.Message is null)
        {
            return;
        }

        var handler = _callbackQueryHandlerFactory.GetHandler(callbackQuery.Data!);

        await handler.HandleAsync(callbackQuery.From.Id, cancellationToken)!;
    }

    public Task UnknownUpdateHandlerAsync(Update update)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}
