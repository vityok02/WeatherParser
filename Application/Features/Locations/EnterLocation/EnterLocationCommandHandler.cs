using Application.Abstract;
using Telegram.Bot.Types;

namespace Application.Features.Locations.EnterLocation;

internal sealed class EnterLocationCommandHandler : ICommandHandler<EnterLocationCommand>
{
    public async Task<Message> Handle(EnterLocationCommand command, CancellationToken cancellationToken)
    {
        // TODO
        return null!;
    }
}
