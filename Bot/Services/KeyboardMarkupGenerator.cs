using Application.Common.Abstract;
using Application.Common.Interfaces.ReplyMarkup;

namespace Bot.Services;

public class KeyboardMarkupGenerator : IKeyboardMarkupGenerator
{
    public IAppReplyMarkup BuildKeyboard(string[] items)
    {
        return new KeyboardMarkup(items);
    }
}