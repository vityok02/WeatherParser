using Domain.Abstract;
using MediatR;

namespace Application.Abstract;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}
