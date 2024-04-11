using Application.Abstract;
using Domain.Users;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Features.Weathers;

public sealed record SendWeatherCommand(long UserId) : ICommand;

internal sealed class SendWeatherCommandHandler : ICommandHandler<SendWeatherCommand>
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserRepository _userRepository;
    private readonly ISender _sender;

    public SendWeatherCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository, ISender sender)
    {
        _botClient = botClient;
        _userRepository = userRepository;
        _sender = sender;
    }

    public async Task<Message> Handle(SendWeatherCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

        return null;
    }
}
