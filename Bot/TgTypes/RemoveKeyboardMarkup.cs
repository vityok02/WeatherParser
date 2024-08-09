﻿using Application.Features.Locations.EnterPlaceName;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.TgTypes;

public class RemoveKeyboardMarkup : IRemoveKeyboardMarkup
{
    public ReplyKeyboardRemove ReplyKeyboardRemove { get; set; }

    public RemoveKeyboardMarkup()
    {
        ReplyKeyboardRemove = new ReplyKeyboardRemove();
    }
}
