using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Translations;
using Domain.Abstract;
using Domain.Users;
namespace Application.Commands.Weathers.Commands.SendWeatherNow;

internal sealed class SendWeatherNowCommandHandler
    : ICommandHandler<SendWeatherNowCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherApiService;
    private readonly IUserTranslationService _translationService;
    private readonly IUserRepository _userRepository;

    public SendWeatherNowCommandHandler(
        IMessageSender messageSender,
        IWeatherApiService weatherApiService,
        IUserTranslationService userTranslationService,
        IUserRepository userRepository)
    {
        _messageSender = messageSender;
        _weatherApiService = weatherApiService;
        _translationService = userTranslationService;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(
        SendWeatherNowCommand command,
        CancellationToken cancellationToken)
    {
        var translation = await _translationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        var lang = await _userRepository
            .GetLanguageAsync(command.UserId, cancellationToken);

        var result = await _weatherApiService
            .GetNowcastAsync(command.Coordinates, lang!.Code);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        var weather = result.Value;
        var formattedWeather = weather
            .ToFormattedWeather(translation);

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: formattedWeather,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
