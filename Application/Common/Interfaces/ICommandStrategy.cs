using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;

namespace Application.Common.Interfaces;

public interface ICommandStrategy
{
    Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancellationToken);
}
