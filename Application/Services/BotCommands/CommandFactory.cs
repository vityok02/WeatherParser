using Application.Abstract;
using Application.Features.Default;
using Application.Features.Locations.DefaultUserState;
using Application.Messaging;
using Application.Services.Commands.Strategy;

namespace Application.Services.Commands;

public class CommandFactory : ICommandFactory
{
    private readonly List<ICommandStrategy> _strategies;

    public CommandFactory(IEnumerable<ICommandStrategy> strategies)
    {
        _strategies = strategies.ToList();
    }

    public async Task<ICommand> Create(
        IMessage message, CancellationToken cancellationToken)
    {
        foreach (var strategy in _strategies)
        {
            var command = await strategy.CreateCommand(message, cancellationToken);

            if (command is not null)
            {
                return command;
            }
        }

        return new DefaultCommand(message.UserId);
    }
}
