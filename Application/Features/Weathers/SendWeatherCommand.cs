using Application.Abstract;
using Application.Features.Locations;
using Domain;
using Domain.Abstract;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Application.Features.Weathers;

public sealed record SendWeatherCommand(long UserId) : ICommand;

internal sealed class SendWeatherCommandHandler : ICommandHandler<SendWeatherCommand>
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IUserRepository _userRepository;
    private readonly IWeatherApiService _weatherApiService;
    private readonly ILogger<SendWeatherCommandHandler> _logger;
    private readonly ISender _sender;

    public SendWeatherCommandHandler(
        ITelegramBotClient telegramBotClient,
        IUserRepository userRepository,
        ISender sender,
        IWeatherApiService weatherApiService,
        ILogger<SendWeatherCommandHandler> logger)
    {
        _telegramBotClient = telegramBotClient;
        _userRepository = userRepository;
        _sender = sender;
        _weatherApiService = weatherApiService;
        _logger = logger;
    }

    public async Task<Result> Handle(SendWeatherCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdWithLocationsAsync(command.UserId, cancellationToken);

        if (user is null)
        {
            var error = new Error("Users.Null");
            _logger.LogError(error.ToString());
            return Result.Failure(error);
        }

        if (!await _userRepository.HasLocationAsync(command.UserId, cancellationToken))
        {
            await _sender.Send(new LocationCommand(command.UserId), cancellationToken);
            return Result.Success();
        }

        var result = await _weatherApiService.GetCurrentWeatherAsync(user!.CurrentLocation!.Coordinates!);

        if (result.IsFailure)
        {
            _logger.LogError(result.Error!.ToString());
            return Result.Failure(result.Error!);
        }

        var weather = result.Value;

        await _telegramBotClient.SendTextMessageAsync(
            command.UserId,
            weather!.ToString()!,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
