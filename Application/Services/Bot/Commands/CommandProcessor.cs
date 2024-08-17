using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Users;
using MediatR;

namespace Application.Services.Bot.Commands;

public class CommandProcessor : ICommandProcessor
{
    private readonly IUserRepository _userRepository;
    private readonly ICommandFactory _commandFactory;
    private readonly ISender _sender;
    private readonly ISessionManager _sessionManager;

    public CommandProcessor(
        IUserRepository repository,
        ICommandFactory commandFactory,
        ISender sender,
        ISessionManager sessionManager)
    {
        _userRepository = repository;
        _commandFactory = commandFactory;
        _sender = sender;
        _sessionManager = sessionManager;
    }

    public async Task ProcessCommand(IMessage message, CancellationToken cancellationToken)
    {
        await _userRepository
            .EnsureCreate(message.UserId, cancellationToken);

        await _userRepository.SaveChangesAsync(cancellationToken);

        var command = await _commandFactory.Create(message, cancellationToken);

        await _sender.Send(command, cancellationToken);
    }
}
