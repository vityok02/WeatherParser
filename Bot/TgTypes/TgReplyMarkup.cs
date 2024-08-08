using Application.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.TgTypes;

public class TgReplyMarkup : IApplicationReplyMarkup, IReplyMarkup
{
    private ReplyKeyboardMarkup ReplyKeyboardMarkup { get; }

    //public TgReplyMarkup()
    //{
    //    ReplyKeyboardMarkup = new ReplyKeyboardMarkup();
    //}
}
