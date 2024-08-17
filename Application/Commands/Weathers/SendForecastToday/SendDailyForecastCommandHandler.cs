using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Services;
using Application.Services;
using Domain.Abstract;

namespace Application.Commands.Weathers.SendForecastToday;

internal sealed class SendDailyForecastCommandHandler
    : ICommandHandler<SendDailyForecastCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherService;
    private readonly TableConverter _converter;
    private readonly ISessionManager _sessionManager;

    public SendDailyForecastCommandHandler(
        IMessageSender messageSender,
        IWeatherApiService weatherService,
        TableConverter tableConverter,
        ISessionManager sessionManager)
    {
        _messageSender = messageSender;
        _weatherService = weatherService;
        _converter = tableConverter;
        _sessionManager = sessionManager;
    }

    public async Task<Result> Handle(
        SendDailyForecastCommand command, CancellationToken cancellationToken)
    {
        var result = await _weatherService
            .GetDailyForecastAsync(command.Coordinates, command.Date);

        if (result.IsFailure)
        {
            await _messageSender.SendTextMessageAsync(
                chatId: command.ChatId,
                text: "Unable to get forecast",
                cancellationToken: cancellationToken);

            return Result.Failure(result.Error!);
        }
        
        var img = _converter.ToTable(result.Value!);

        await _messageSender.SendPhotoAsync(
            chatId: command.ChatId,
            photo: img,
            cancellationToken: cancellationToken);

        var session = _sessionManager.GetOrCreateSession(command.ChatId);
        session.Remove("state");

        return Result.Success();
    }
}
