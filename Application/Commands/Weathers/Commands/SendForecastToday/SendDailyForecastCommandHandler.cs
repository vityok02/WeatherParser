using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Services;
using Application.Services;
using Domain.Abstract;
using Domain.Users;

namespace Application.Commands.Weathers.Commands.SendForecastToday;

internal sealed class SendDailyForecastCommandHandler
    : ICommandHandler<SendDailyForecastCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherService;
    private readonly TableConverter _converter;
    private readonly ISessionManager _sessionManager;
    private readonly IUserTranslationService _translationService;
    private readonly IUserRepository _userRepository;

    public SendDailyForecastCommandHandler(
        IMessageSender messageSender,
        IWeatherApiService weatherService,
        TableConverter tableConverter,
        ISessionManager sessionManager,
        IUserTranslationService translationService,
        IUserRepository userRepository)
    {
        _messageSender = messageSender;
        _weatherService = weatherService;
        _converter = tableConverter;
        _sessionManager = sessionManager;
        _translationService = translationService;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(
        SendDailyForecastCommand command,
        CancellationToken cancellationToken)
    {
        var translation = await _translationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        var language = await _userRepository
            .GetLanguageAsync(command.UserId, cancellationToken);

        var result = await _weatherService
            .GetDailyForecastAsync(command.Coordinates, language.Code, command.Date);

        if (result.IsFailure)
        {
            await _messageSender.SendTextMessageAsync(
                chatId: command.UserId,
                text: translation.Messages["ForecastFail"],
                cancellationToken: cancellationToken);

            return Result.Failure(result.Error!);
        }

        var img = _converter.ToTable(result.Value!, translation);

        await _messageSender.SendPhotoAsync(
            chatId: command.UserId,
            photo: img,
            cancellationToken: cancellationToken);

        var session = _sessionManager
            .GetOrCreateSession(command.UserId);
        session.Remove("state");

        return Result.Success();
    }
}
