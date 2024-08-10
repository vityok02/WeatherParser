using Application.Abstract;
using Application.Messaging;

namespace Application.Services.Commands;

public interface ICommandFactory
{
    Task<ICommand> Create(IMessage message, CancellationToken cancellationToken);
}