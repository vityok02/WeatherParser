using Application.Abstract;
using Application.Messaging;

namespace Application.Interfaces;

public interface ICommandProcessor
{
    Task ProcessCommand(IMessage message, CancellationToken cancellationToken);
}