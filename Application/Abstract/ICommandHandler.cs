using Domain.Abstract;
using MediatR;
using Telegram.Bot.Types;

namespace Application.Abstract;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}
