﻿using Application.Constants;
using Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Services;

public class LocationRequestHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserStateRepository _userStateRepository;

    public LocationRequestHandler(ITelegramBotClient botClient, IUserStateRepository userStateRepository)
    {
        _botClient = botClient;
        _userStateRepository = userStateRepository;
    }

    public async Task RequestLocation(long userId, CancellationToken cancellationToken)
    {
        _userStateRepository.SetState(userId, UserState.SetLocation);

        await _botClient.SendTextMessageAsync(
            chatId: userId,
            text: "Click the button or enter your place manually",
            replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestLocation("Send geolocation")),
            cancellationToken: cancellationToken);
    }
}