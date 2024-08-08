using Application.Constants;
using Application.Interfaces;
using Domain.Abstract;
using Telegram.Bot;

namespace Application.Features.CallbackQueryCommands.SendPlaceNameRequestCommand;

internal class SendPlaceNameRequestCommandHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserStateRepository _cachedUserStateRepository;

    public SendPlaceNameRequestCommandHandler(ITelegramBotClient botClient, IUserStateRepository cachedUserStateRepository)
    {
        _botClient = botClient;
        _cachedUserStateRepository = cachedUserStateRepository;
    }

    public async Task<Result> Handle(long userId, CancellationToken cancellationToken)
    {
        _cachedUserStateRepository.SetState(userId, UserState.EnterLocation);

        await _botClient.SendTextMessageAsync(
            chatId: userId,
            text: "Enter your place",
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
