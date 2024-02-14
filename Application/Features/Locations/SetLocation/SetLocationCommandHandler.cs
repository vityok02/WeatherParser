using Application.Abstract;
using Telegram.Bot.Types;

namespace Application.Features.Locations.SetLocation;

internal class SetLocationCommandHandler : ICommandHandler<SetLocationCommand>
{
    public Task<Message> Handle(SetLocationCommand command, CancellationToken cancellationToken)
    {
        // TODO:
        return null!;
    }
}
