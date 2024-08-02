using Application.Abstract;
using Domain;
using Telegram.Bot.Types;

namespace Application.Locations.SetLocationFromRequest;

internal class SetLocationFromRequestCommandHandler : ICommandHandler<SetLocationFromRequestCommand>
{
    public Task<Result> Handle(SetLocationFromRequestCommand command, CancellationToken cancellationToken)
    {
        // TODO:
        throw new NotImplementedException();
    }
}
