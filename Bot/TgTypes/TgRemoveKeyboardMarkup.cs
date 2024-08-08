using Application.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.TgTypes;

public class TgRemoveKeyboardMarkup : ReplyKeyboardRemove, IReplyMarkup, IReplyKeyboardRemove
{

}
