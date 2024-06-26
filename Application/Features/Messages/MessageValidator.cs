﻿using Application.Interfaces;
using Domain;
using Telegram.Bot.Types;

namespace Application.Features.Messages;

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
            validationResult.Errors.Add("User cannot null");
        }
        if (message.Text is null)
        {
            validationResult.Errors.Add("Message text cannot be null");
        }

        return validationResult;
    }
}
