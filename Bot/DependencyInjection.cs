﻿using Application.Commands.Default;
using Application.Common.Abstract;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Application.Keyboard;
using Bot.BotHandlers;
using Bot.Messages;
using Bot.Services;
using Bot.TgTypes;
using Telegram.Bot.Types;

namespace Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddScoped<ReceiverService>()
            .AddHostedService<PollingService>()
            .AddScoped<UpdateHandler>()
            .AddScoped<IMessageHandler, MessageHandler>()
            .AddScoped<DefaultHandler>()
            .AddScoped<IValidator<Message>, MessageValidator>()
            .AddScoped<IMessageSender, TelegramMessageSender>()
            .AddScoped<IAppReplyMarkup, TgReplyMarkup>()
            .AddScoped<TelegramFileAdapter>()
            .AddScoped<IKeyboardMarkupGenerator, KeyboardMarkupGenerator>()
            .AddScoped<IRemoveKeyboardMarkup, RemoveKeyboardMarkup>()
            .AddScoped<IDefaultKeyboardFactory, DefaultKeyboardFactory>()
            ;

        return services;
    }
}
