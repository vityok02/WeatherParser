using Application.Abstract;
using Application.Messaging;
using Domain.Abstract;
namespace Application.Features.Weathers.SendWeatherNow;

internal sealed class SendWeatherNowCommandHandler : ICommandHandler<SendWeatherNowCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherApiService;

    public SendWeatherNowCommandHandler(
        IMessageSender messageSender,
        IWeatherApiService weatherApiService)
    {
        _messageSender = messageSender;
        _weatherApiService = weatherApiService;
    }

    public async Task<Result> Handle(SendWeatherNowCommand command, CancellationToken cancellationToken)
    {
        var result = await _weatherApiService.GetNowcastAsync(command.Coordinates);

        if (result.IsFailure)
        {
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
