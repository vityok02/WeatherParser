using Application.Abstract;
using Application.Messaging;
using Application.Services;
using Domain.Abstract;

namespace Application.Features.Weathers.SendForecastToday;

internal sealed class SendDailyForecastCommandHandler : ICommandHandler<SendDailyForecastCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherService;
    private readonly TableConverter _converter;

    public SendDailyForecastCommandHandler(
        IMessageSender messageSender,
        IWeatherApiService weatherService,
        TableConverter tableConverter)
    {
        _messageSender = messageSender;
        _weatherService = weatherService;
        _converter = tableConverter;
    }

    public async Task<Result> Handle(SendDailyForecastCommand command, CancellationToken cancellationToken)
    {
        var result = await _weatherService.GetDailyForecastAsync(command.Coordinates, command.Date);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }
        
        var img = _converter.ToTable(result.Value.DailyForecast!);

        await _messageSender.SendPhotoAsync(
            chatId: command.ChatId,
            photo: img,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
