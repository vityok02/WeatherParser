using Application.Abstract;
using Domain.Abstract;
using Domain.Locations;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Application.Features.Weathers.SendForecastToday;

public sealed record SendForecastTodayCommand(long ChatId, Coordinates Coordinates) : ICommand;

internal sealed class SendForecastTodayCommandHandler : ICommandHandler<SendForecastTodayCommand>
{
    private readonly ITelegramBotClient _botClient;
    private readonly IWeatherApiService _weatherService;
    private readonly ILogger<SendForecastTodayCommandHandler> _logger;

    public SendForecastTodayCommandHandler(
        ITelegramBotClient botClient,
        IWeatherApiService weatherService,
        ILogger<SendForecastTodayCommandHandler> logger)
    {
        _botClient = botClient;
        _weatherService = weatherService;
        _logger = logger;
    }

    public async Task<Result> Handle(SendForecastTodayCommand command, CancellationToken cancellationToken)
    {
        var result = await _weatherService.GetForecastAsync(command.Coordinates, 1);

        if (result.IsFailure)
        {
            _logger.LogError(result.Error!.ToString());
            return Result.Failure(result.Error);
        }

        var img = ConvertToTable.ToTable(result.Value!);

        await _botClient.SendPhotoAsync(
            chatId: command.ChatId,
            photo: img,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
