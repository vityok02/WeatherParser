using Application.Abstract;
using Telegram.Bot.Types;

namespace Application.Features.Locations.DefaultUserState;

internal class DefaultUserStateCommandHandler : ICommandHandler<DefaultUserStateCommand>
{
    public Task<Message> Handle(DefaultUserStateCommand command, CancellationToken cancellationToken)
    {
        // TODO:
        throw new NotImplementedException();
    }
}
