using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Services;
using Domain.Abstract;
using Domain.Users;

namespace Application.Commands.Weathers.Commands.SendMultiDayForecast;

internal sealed class SendMultidayForecastCommandHandler
    : ICommandHandler<SendMultidayForecastCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IWeatherApiService _weatherApiService;
    private readonly IUserRepository _userRepository;

    public SendMultidayForecastCommandHandler(
        IMessageSender messageSender,
        IWeatherApiService weatherApiService,
        IUserRepository userRepository)
    {
        _weatherApiService = weatherApiService;
        _messageSender = messageSender;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(
        SendMultidayForecastCommand command,
        CancellationToken cancellationToken)
    {
        var lang = await _userRepository
            .GetLanguageAsync(command.UserId, cancellationToken);

        var result = await _weatherApiService
            .GetMultiDayForecastAsync(command.Coordinates, lang.Code, command.Days);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        var multidayForecast = result.Value;

        return Result.Success();
    }
}
