using Application.Abstract;
using Application.Interfaces;
using Application.Services;
using Domain.Abstract;
using Microsoft.Extensions.Logging;

namespace Application.Features.Weathers.SendForecastToday;

internal sealed class SendForecastTodayCommandHandler : ICommandHandler<SendForecastTodayCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherService;
    private readonly ILogger<SendForecastTodayCommandHandler> _logger;
    private readonly TableConverter _converter;

    public SendForecastTodayCommandHandler(
        IMessageSender messageSender,
        IWeatherApiService weatherService,
        ILogger<SendForecastTodayCommandHandler> logger,
        TableConverter tableConverter)
    {
        _messageSender = messageSender;
        _weatherService = weatherService;
        _logger = logger;
        _converter = tableConverter;
    }

    public async Task<Result> Handle(SendForecastTodayCommand command, CancellationToken cancellationToken)
    {
        var result = await _weatherService.GetForecastAsync(command.Coordinates, 1);

        if (result.IsFailure)
        {
            _logger.LogError(result.Error!.ToString());
            return Result.Failure(result.Error);
        }
        
        var img = _converter.ToTable(result.Value!);

        await _messageSender.SendPhotoAsync(
            chatId: command.ChatId,
            photo: img,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
