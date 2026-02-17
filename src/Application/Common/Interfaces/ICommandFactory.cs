using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;

namespace Application.Common.Interfaces;

public interface ICommandFactory
{
    Task<ICommand> Create(IMessage message, CancellationToken cancellationToken);
}