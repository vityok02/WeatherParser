using Application.Abstract;
using Application.Constants;
using Application.Interfaces;
using Domain.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Features.CallbackQueryCommands.SendPlaceNameRequestCommand;

internal class SendPlaceNameRequestCommandHandler : ICommandHandler<SendPlaceNameRequestCommand>
{
    private readonly ITelegramBotClient _botClient;
    private readonly ICachedUserStateRepository _cachedUserStateRepository;

    public SendPlaceNameRequestCommandHandler(ITelegramBotClient botClient, ICachedUserStateRepository cachedUserStateRepository)
    {
        _botClient = botClient;
        _cachedUserStateRepository = cachedUserStateRepository;
    }

    public async Task<Result> Handle(SendPlaceNameRequestCommand command, CancellationToken cancellationToken)
    {
        _cachedUserStateRepository.SetCache(command.UserId, UserState.EnterLocation);

        await _botClient.SendTextMessageAsync(
            chatId: command.UserId,
            text: "Enter your place",
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
