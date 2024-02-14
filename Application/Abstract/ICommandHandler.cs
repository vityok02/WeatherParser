using MediatR;
using Telegram.Bot.Types;

namespace Application.Abstract;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Message>
    where TCommand : ICommand
{
    Task<Message> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Message>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}