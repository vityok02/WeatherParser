using Application.Abstract;
using Application.Features.Locations;
using Domain.Abstract;
using Domain.Locations;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Application.Features.Weathers.SendWeatherNow;

internal sealed class SendWeatherNowCommandHandler : ICommandHandler<SendWeatherNowCommand>
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IUserRepository _userRepository;
    private readonly IWeatherApiService _weatherApiService;
    private readonly ILogger<SendWeatherNowCommandHandler> _logger;
    private readonly ISender _sender;

    public SendWeatherNowCommandHandler(
        ITelegramBotClient telegramBotClient,
        IUserRepository userRepository,
        ISender sender,
        IWeatherApiService weatherApiService,
        ILogger<SendWeatherNowCommandHandler> logger)
    {
        _telegramBotClient = telegramBotClient;
        _userRepository = userRepository;
        _sender = sender;
        _weatherApiService = weatherApiService;
        _logger = logger;
    }

    public async Task<Result> Handle(SendWeatherNowCommand command, CancellationToken cancellationToken)
    {
        var result = await _weatherApiService.GetCurrentWeatherAsync(command.Coordinates);

        if (result.IsFailure)
        {
            _logger.LogError(result.Error!.ToString());
            return Result.Failure(result.Error!);
        }

        var weather = result.Value;

        await _telegramBotClient.SendTextMessageAsync(
            chatId: command.ChatId,
            text: weather!.ToString()!,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
