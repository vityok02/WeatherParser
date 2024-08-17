using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Telegram.Bot.Types;

namespace Bot.Messages;

public class MessageHandler : IMessageHandler
{
    private readonly ILogger<MessageHandler> _logger;
    private readonly IValidator<Message> _validator;
    private readonly ICommandProcessor _commandProcessor;

    public MessageHandler(
        ILogger<MessageHandler> logger,
        IValidator<Message> validator,
        ICommandProcessor commandProcessor)
    {
        _logger = logger;
        _validator = validator;
        _commandProcessor = commandProcessor;
    }

    public async Task HandleMessage(Message message, CancellationToken cancellationToken)
    {
        _logger
            .LogInformation("Receive message type: {MessageType}", message.Type);

        var validationResult = _validator.Validate(message);
        if (validationResult.IsError)
        {
            _logger
                .LogError("Invalid message: {validationResult}", validationResult.ToString());
        }

        IMessage appMessage = new TelegramMessageAdapter(message);

        await _commandProcessor.ProcessCommand(appMessage, cancellationToken);
    }
}
