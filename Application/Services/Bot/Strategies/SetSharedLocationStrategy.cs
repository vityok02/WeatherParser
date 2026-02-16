using Application.Commands.Locations.SetSharedLocation;
using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;

namespace Application.Services.Bot.Strategies;

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
