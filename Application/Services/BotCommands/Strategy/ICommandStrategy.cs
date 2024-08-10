using Application.Abstract;
using Application.Messaging;

namespace Application.Services.Commands.Strategy;

public interface ICommandStrategy
{
    Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancellationToken);
}
