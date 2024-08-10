using Application.Abstract;
using Application.Interfaces;
using Application.Messaging;
using Domain.Users;
using MediatR;

namespace Application.Services.Commands;

public class CommandProcessor : ICommandProcessor
{
    private readonly IUserRepository _userRepository;
    private readonly ICommandFactory _commandFactory;
    private readonly ISender _sender;

    public CommandProcessor(IUserRepository repository, ICommandFactory commandFactory, ISender sender)
    {
        _userRepository = repository;
        _commandFactory = commandFactory;
        _sender = sender;
    }

    public async Task ProcessCommand(IMessage message, CancellationToken cancellationToken)
    {
        await _userRepository
            .EnsureCreate(message.UserId, cancellationToken);

        var command = await _commandFactory.Create(message, cancellationToken);

        await _sender.Send(command, cancellationToken);
    }
}
