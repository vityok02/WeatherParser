using Application.Abstract;
using Application.Features.Weathers.SendForecastToday;
using Application.Interfaces;
using Application.Messaging;
using Common.Constants;
using Domain.Abstract;
using Domain.Users;
using MediatR;

namespace Application.Features.Weathers.EnterDay;

public record EnterDayCommand(long ChatId, string Text) : ICommand;

internal class EnterDayCommandHandler
    : ICommandHandler<EnterDayCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;
    private readonly IUserStateRepository _userStateRepository;

    public EnterDayCommandHandler(
        ISender sender,
        IMessageSender messageSender,
        IUserRepository userRepository,
        IUserStateRepository userStateRepository)
    {
        _messageSender = messageSender;
        _sender = sender;
        _userRepository = userRepository;
        _userStateRepository = userStateRepository;
    }

    public async Task<Result> Handle(EnterDayCommand command, CancellationToken cancellationToken)
    {
        var day = Days.Value.GetValueOrDefault(command.Text);

        if (day == DateTime.MinValue)
        {
            await _messageSender.SendKeyboardAsync(
                command.ChatId,
                "Try again",
                cancellationToken);

            return Result.Failure("Day.NotValid", "Value from user is not valid");
        }

        var user = await _userRepository.GetByIdWithLocationsAsync(command.ChatId, cancellationToken);
        var coordinates = user!.CurrentLocation!.Coordinates;

        _userStateRepository.RemoveState(command.ChatId);
        _userStateRepository.SetState(command.ChatId, UserState.GetDailyForecast);

        //await _sender.Send(new SendDailyForecastCommand(command.ChatId, coordinates, day), cancellationToken);

        return Result.Success();
    }
}
