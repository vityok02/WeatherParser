using Application.Common.Abstract;
using Application.Messaging;

namespace Application.Services.Commands.Strategies;

public interface ICommandStrategy
{
    Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancellationToken);
}
