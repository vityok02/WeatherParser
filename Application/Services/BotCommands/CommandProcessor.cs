using Application.Abstract;
using Application.Interfaces;
using Application.Messaging;
using Domain.Abstract;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Commands;

public class CommandProcessor : ICommandProcessor
{
    private readonly IUserRepository _userRepository;
    private readonly ICommandFactory _commandFactory;
    private readonly ISender _sender;
    private readonly ILogger<CommandProcessor> _logger;

    public CommandProcessor(
        IUserRepository repository,
        ICommandFactory commandFactory,
        ISender sender,
        ILogger<CommandProcessor> logger)
    {
        _userRepository = repository;
        _commandFactory = commandFactory;
        _sender = sender;
        _logger = logger;
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
