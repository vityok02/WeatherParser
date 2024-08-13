using Application.Abstract;
using Application.Messaging;
using Domain.Abstract;
using Domain.Locations;

namespace Application.Features.Weathers.SendMultiDayForecast;

public sealed record SendMultidayForecastCommand(string ChatId, int Days, Coordinates Coordinates) : ICommand;

internal sealed class SendMultidayForecastCommandHandler
    : ICommandHandler<SendMultidayForecastCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherApiService;

    public SendMultidayForecastCommandHandler(
        IMessageSender messageSender, 
        IWeatherApiService weatherApiService)
    {
        _weatherApiService = weatherApiService;
        _messageSender = messageSender;
    }

    public async Task<Result> Handle(
        SendMultidayForecastCommand command, CancellationToken cancellationToken)
    {
        var result = await _weatherApiService
            .GetMultiDayForecastAsync(command.Coordinates, command.Days);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        var multidayForecast = result.Value;

        return Result.Success();
    }
}
