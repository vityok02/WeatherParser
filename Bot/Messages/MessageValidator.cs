using Application.Abstract;
using Domain.Abstract;
using Telegram.Bot.Types;

namespace Bot.Messages;

public class MessageValidator : IValidator<Message>
{
    public ValidationResult Validate(Message message)
    {
        var validationResult = new ValidationResult();

        if (message is null)
        {
            validationResult.Errors.Add("Message cannot be null");
            return validationResult;
        }
        if (message.From is null)
        {
            validationResult.Errors.Add("User cannot be null");
        }
        if (message.Text is null)
        {
            validationResult.Errors.Add("Message text cannot be null");
        }

        return validationResult;
    }
}
