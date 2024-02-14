using Application.Abstract;
using Telegram.Bot.Types;

namespace Application.Locations.SetLocationFromRequest;

internal class SetLocationFromRequestCommandHandler : ICommandHandler<SetLocationFromRequestCommand>
{
    public Task<Message> Handle(SetLocationFromRequestCommand command, CancellationToken cancellationToken)
    {
        // TODO:
        throw new NotImplementedException();
    }
}
