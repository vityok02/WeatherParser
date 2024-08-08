using Application.Abstract;
using Application.Interfaces;
using Domain.Abstract;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Application.Features.Weathers.SendWeatherNow;

internal sealed class SendWeatherNowCommandHandler : ICommandHandler<SendWeatherNowCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherApiService;
    private readonly ILogger<SendWeatherNowCommandHandler> _logger;

    public SendWeatherNowCommandHandler(
        IMessageSender messageSender,
        IWeatherApiService weatherApiService,
        ILogger<SendWeatherNowCommandHandler> logger)
    {
        _messageSender = messageSender;
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

        await _messageSender.SendTextMessageAsync(
            chatId: command.ChatId,
            text: weather!.ToString()!,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
