using Application.Abstract;
using Domain;

namespace Application.Features.Locations.DefaultUserState;

internal class DefaultUserStateCommandHandler : ICommandHandler<DefaultUserStateCommand>
{
    public Task<Result> Handle(DefaultUserStateCommand command, CancellationToken cancellationToken)
    {
        // TODO:
        throw new NotImplementedException();
    }
}
