using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;
using Domain.Abstract;
using Domain.Users;

namespace Application.Commands.Locations.ViewLocation;
public sealed record ViewLocationCommand(long UserId)
    : ICommand;

internal sealed class ViewLocationCommandHandler
    : ICommandHandler<ViewLocationCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IUserRepository _userRepository;
    private readonly IUserTranslationService _userTranslationService;

    public ViewLocationCommandHandler(
        IUserRepository userRepository,
        IMessageSender messageSender,
        IUserTranslationService userTranslationService)
    {
        _userRepository = userRepository;
        _messageSender = messageSender;
        _userTranslationService = userTranslationService;
    }

    public async Task<Result> Handle(
        ViewLocationCommand command,
        CancellationToken cancellationToken)
    {
        var translation = await _userTranslationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        var user = await _userRepository
            .GetByIdWithLocationsAsync(command.UserId, cancellationToken);

        await _messageSender.SendTextMessageAsync(
            command.UserId,
            user!.CurrentLocation?.Name ?? translation.Messages["NoLocation"],
            cancellationToken);

        return Result.Success();
    }
}
