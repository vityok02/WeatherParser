using Application.Abstract;
using Application.Locations.SetLocationFromRequest;
using Application.Messaging;

namespace Application.Services.Commands.Strategy;

public class SetSharedLocationStrategy : ICommandStrategy
{
    public Task<ICommand> CreateCommand(
        IMessage message, CancellationToken cancellationToken)
    {
        if (message.Coordinates is not null)
        {
            var command =
                new SetSharedLocationCommand(message.UserId, message.Coordinates);

            return Task
                .FromResult<ICommand>(command);
        }

        return Task
            .FromResult<ICommand>(null!);
    }
}
