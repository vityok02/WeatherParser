using Application.Common.Interfaces.Messaging;

namespace Application.Common.Interfaces;

public interface ICommandProcessor
{
    Task ProcessCommand(IMessage message, CancellationToken cancellationToken);
}