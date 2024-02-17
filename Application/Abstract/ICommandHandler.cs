using MediatR;
using Telegram.Bot.Types;

namespace Application.Abstract;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Message>
    where TCommand : ICommand
{
    Task<Message> Handle(TCommand command, CancellationToken cancellationToken);
}
