using Application.Commands.Default;
using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Common.Constants;

namespace Application.Services.Bot.Commands;

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
        if (message.Text == BotCommand.Start)
        {
            return new DefaultCommand(message.UserId);
        }

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
